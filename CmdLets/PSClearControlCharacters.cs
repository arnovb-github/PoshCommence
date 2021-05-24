﻿using PoshCommence.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Vovin.CmcLibNet;
using Vovin.CmcLibNet.Database;
using Vovin.CmcLibNet.Database.Metadata;

/*
This cmdlet clears control characters from Commence fields,
with the exception of \r\n (standard Windows line-ending) in Large Text fields.

Use it when a field that contains such characters throw off API calls or exports.

Examples:

-when exporting connected data from connections, the delimiter that Commence uses
is a \n (UNIX-style line-ending). If connected data fields contains a UNIX-style line-ending
as part of its fieldvalue, this throws off the export. This is most obvious with multi-line fields,
but URL fields in Commence are also multiline (god knows why).

-Data Files can contain \t (tab) characters. This throws off enrolls, among other things.

There are probably more examples.

In addition, The Commence API allows for writing control characters to fields
that do not support so via the UI.

In general, when editing data using the Commence API, Commence does a rather poor job of 
checking input compared to the UI.
*/

namespace PoshCommence.CmdLets
{
    [Cmdlet(VerbsCommon.Clear, "CmcControlCharacters",
        SupportsShouldProcess = true,
        ConfirmImpact = ConfirmImpact.None)] // ConfirmImpact = None will supress a superfluous prompt that may occur depending on user preference settings
    public class ClearCmcControlCharacters : PSCmdlet
    {
        private const string TARGET = "Commence database";
        private Dictionary<string, Dictionary<char, string>> rules;
        private ChangeLog changeLog;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        private string categoryName;

        [Parameter(Position = 1)]
        public string LogDir
        {
            get { return logDir; }
            set { logDir = value; }
        }
        private string logDir;

        private int maxRows = 100;

        [Parameter]
        [ValidateRange(1, 1000)]
        public int MaxRows // Should only be changed when debugging, set it to 1 in that case.
        {
            get { return maxRows; }
            set { maxRows = value; }
        }

        // this should only ever be used with extreme caution
        [Parameter(Position = 2)]
        [ValidateLength(1, 8)]
        public string ColumnDelimiter
        {
            get { return colDelimiter; }
            set { colDelimiter = value; }
        }
        private string colDelimiter;

        [Parameter]
        public SwitchParameter Force
        {
            get { return force; }
            set { force = value; }
        }
        private bool force;

        string logFile = string.Empty;
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            logFile = GetLogPath();
            rules = GetColumnReplacementRules(categoryName);
            changeLog = new ChangeLog()
            {
                Date = DateTime.Now,
                CategoryName = categoryName
            };
        }

        protected override void ProcessRecord()
        {
            if (ShouldProcess(TARGET)) // for use with WhatIf
            {
                if (Force || ShouldContinue("Do you want to continue?",
                    $"Remove control characters from text-fields in category '{categoryName}'." +
                    $" This action cannot be undone.\n" +
                    $"It is recommended that you disable workgroup syncing and backup your database before you run this command."))
                {
                    try
                    {
                        ClearControlCharactersFromCategory(this.CategoryName, true);
                    }
                    catch (CommenceCOMException ex)
                    {
                        var er = new ErrorRecord(ex, "Commence database write failed", ErrorCategory.InvalidOperation, TARGET);
                        WriteVerbose($"See '{logFile}' for details.\n" +
                            $"To pinpoint the offending row set the MaxRows property to 1. This will make the changes to the Commence database " +
                            $"1 item at a time. Note that this is significantly slower!");
                        ThrowTerminatingError(er);
                    }
                }
                else
                {
                    // a little harsh?
                    ThrowTerminatingError(new ErrorRecord(new Exception(
                        "Cancelled by user"), "Error ID",
                        ErrorCategory.OperationStopped, TARGET));
                }
            }
            else // -WhatIf portion
            {
                ClearControlCharactersFromCategory(this.CategoryName, false);
                WriteObject(GetSummaryFromLog(changeLog));
                WriteVerbose($"See '{logFile}' for details.");
            }
        }

        private void ClearControlCharactersFromCategory(string categoryName, bool doCommit)
        {
            using (var db = new CommenceDatabase())
            using (var cur = db.GetCursor(categoryName, CmcCursorType.Category, CmcOptionFlags.UseThids)) // TODO we can optimize this by eliminating connections. Or can we? No we cannot, we can only limit it after getting it.
            {
                // use the keys in rules to limit our cursor
                // the keys represent the fieldnames to be processed
                cur.Columns.AddDirectColumns(rules.Keys.ToArray());
                cur.Columns.Apply();
                int totalRows = cur.RowCount;
                changeLog.DatabaseName = db.Name;

                for (int rows = 0; rows < totalRows; rows += maxRows)
                {
                    using (var ers = cur.GetEditRowSet(maxRows))
                    {
                        int rowCount = ers.RowCount;
                        var rowsetIsDirty = ProcessRows(ers, rowCount);
                        if (doCommit && rowsetIsDirty)
                        {
                            int result = ers.Commit();
                            if (result != 0) // database write failed
                            {
                                try
                                {
                                    changeLog.WriteToFile(logFile);
                                }
                                finally
                                {
                                    throw new CommenceCOMException($"Commit method failed on rowset between rows {rows} and {rows + rowCount}.");
                                }
                            }
                        }
                    }
                    if (rows % 100 == 0)
                    {
                        WriteProgress(new ProgressRecord(0, $"{rows} of {totalRows}", "Commence rows processed."));
                    }
                }
                changeLog.WriteToFile(logFile); // all went peachy
            }
        }

        // violates S in SOLID
        private bool ProcessRows(ICommenceEditRowSet ers, int rowCount)
        {
            bool retval = false;
            
            for (int row = 0; row < rowCount; row++) // process Commence rows
            {
                var changeResults = new List<FieldModification>();
                var rowId = ers.GetRowID(row, CmcOptionFlags.UseThids);
                // let CmcLibNet do magic.
                string[] rowValues = string.IsNullOrEmpty(colDelimiter)
                    ? ers.GetRow(row).Cast<string>().ToArray()
                    : ers.GetRow(row, colDelimiter).Cast<string>().ToArray();
                // the keys correspond to columns in the cursor
                for (int column = 0; column < rules.Keys.Count; column++) // process fields in row
                {
                    var oldValue = rowValues[column];
                    // rules.Values.ElementAt(c) contains the dictionary of rules for character replacement.
                    var newValue = ReplaceControlCharacters(oldValue, rules.Values.ElementAt(column));
                    if (!oldValue.Equals(newValue)) // TODO can we make this faster?
                    {
                        // TODO try/catch this?
                        var result = ers.ModifyRow(row, column, newValue, CmcOptionFlags.Default); // ModifyRow will truncate strings.
                        if (result != 0)
                        {
                            throw new CommenceCOMException($"Commence method EditRowSet.ModifyRow() failed on row {row}, column {column}.");
                        }
                        retval = true;
                        // add new result to temporary changeset
                        changeResults.Add(new FieldModification(
                            // notice we pass rowValues[0] as the itemname
                            // that is because the Name field is always the first field to be returned from Commence,
                            // and it is also always present in our test.
                            // it is therefore a little brittle.
                            rowValues[0], // itemname
                            rules.Keys.ElementAt(column), // fieldname
                            oldValue,
                            newValue,
                            result));
                    }
                }
                // add edits made to the row to change log
                if (changeResults.Any())
                {
                    changeLog.ModifiedRows.Add(rowId, changeResults);
                }
            }
            return retval;
        }

        private Dictionary<string, Dictionary<char, string>> GetColumnReplacementRules(string categoryName)
        {
            var retval = new Dictionary<string, Dictionary<char, string>>();
            using (var db = new CommenceDatabase())
            {
                var fieldNames = db.GetFieldNames(categoryName);
                foreach (string f in fieldNames)
                {
                    var def = db.GetFieldDefinition(categoryName, f);
                    // should we process this field?
                    // we do this by looking if there are any replacement characters
                    // in the dictionary for this fieldtype
                    var dict = GetReplacementCharsForField(def);
                    if (dict.Keys.Any())
                    {
                        retval.Add(f, dict);
                    }
                }
            }
            return retval;
        }

        /// <summary>
        /// Sets the replacement rules for the fieldtypes. 
        /// If no rules are specified, the field will not be processrd.
        /// </summary>
        /// <param name="d">Commence field definition</param>
        /// <returns>Dictionary of chars to replace</returns>
        private Dictionary<char, string> GetReplacementCharsForField(ICommenceFieldDefinition d)
        {
            var retval = new Dictionary<char, string>();
            switch (d.Type)
            {
                case CommenceFieldType.Name:
                case CommenceFieldType.Datafile:
                case CommenceFieldType.Telephone:
                    retval.Add('\t', " ");
                    retval.Add('\n', string.Empty); // not strictly needed, it just means remove
                    retval.Add('\r', string.Empty);
                    return retval;
                case CommenceFieldType.Email:
                case CommenceFieldType.URL:
                    retval.Add('\t', string.Empty);
                    retval.Add('\n', string.Empty);
                    retval.Add('\r', string.Empty);
                    return retval;
                case CommenceFieldType.Text:
                    retval.Add('\t', " "); // debatable but may be useful for tab-delimited exports
                    // A Commence large text field that is later reset to a smaller size will retain \r\n
                    // in effect it stays a 'Memo' field, only smaller.
                    // A text field that started life as a text field cannot have \n or \r written to it
                    // not even by copy/paste or API.
                    // Therefore, we cannot get information on the field from just
                    // looking at its length. Hmm.
                    // the only viable way to move forward I think is
                    // to remove stand-alone linefeeds (aka Unix-style line-endings)
                    retval.Add('\n', "\r\n");
                    retval.Add('\r', string.Empty);
                    return retval;
                default:
                    return retval;
            }
        }

        private string ReplaceControlCharacters(string str, Dictionary<char, string> replacementString)
        {
            // some speed optimizations: cast to chararray and loop string just once
            char[] c = str.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int length = str.Length;
            for (int i = 0; i < length; i++)
            {
                if (c[i] > 31)
                {
                    sb.Append(c[i]);
                    continue;
                }
                if (c[i] == 13 || c[i] == 9 || c[i] == 10)
                {
                    sb.Append(replacementString[c[i]]); // no key exists check, we simply expect it to be there!
                }
            }
            return sb.ToString();
        }

        private string GetLogPath()
        {
            string fileName = $"{MyInvocation.MyCommand}_{DateTime.Now.ToString("yyyyMMddHHMMss")}.log";
            string path;
            if (Directory.Exists(LogDir))
            {
                path = Path.Combine(LogDir, fileName);
            }
            else
            {
                string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                if (!Directory.Exists(logPath)) { Directory.CreateDirectory(logPath); }
                path = Path.Combine(logPath, fileName);
            }
            return path;
        }

        private PSObject GetSummaryFromLog(ChangeLog log)
        {
            int rows = log.ModifiedRows.Keys.Count();
            var fields = log.ModifiedRows.Sum(s => s.Value.Count());
            var o = new PSObject();
            o.Members.Add(new PSNoteProperty("Category", log.CategoryName));
            o.Members.Add(new PSNoteProperty("Affected rows", rows));
            o.Members.Add(new PSNoteProperty("Affected fields", fields));
            return o;
        }
    }
}
