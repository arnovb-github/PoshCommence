using PoshCommence.Base;
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
but URL fields in Commence are also multiline.

-Data Files can contain \t (tab) characters. This can crash client enrolls, among other things.

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
        private ChangeLog changeLog;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipelineByPropertyName = true)]
        [ArgumentCompleter(typeof(CategoryNameArgumentCompleter))]
        [Alias("c")]
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }
        private string categoryName;

        [Parameter(Position = 1)]
        [Alias("l")]
        public string LogDir
        {
            get { return logDir; }
            set { logDir = value; }
        }
        private string logDir;

        private int maxRows = 100;

        [Parameter]
        [ValidateRange(1, 1000)]
        [Alias("m")]
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
            using (var db = new CommenceDatabase())
            {
                categoryNames = db.GetCategoryNames();
            }
            logFile = GetLogPath();
            
            changeLog = new ChangeLog()
            {
                Date = DateTime.Now
            };
        }

        IEnumerable<string> categoryNames;
        protected override void ProcessRecord()
        {
            // do not process categories that arent in the database
            if (!categoryNames.Contains(this.CategoryName))
            {
                WriteWarning($"Skipped category '{this.CategoryName}': it does not exist in the database.");
                return;
            }
            if (ShouldProcess(TARGET)) // for use with WhatIf
            {
                if (Force || ShouldContinue("Do you want to continue?",
                    $"Remove control characters from text-fields in category '{this.CategoryName}'." +
                    $" This action cannot be undone.\n" +
                    $"It is recommended that you disable workgroup syncing and backup your database before you run this command.\n" +
                    $"It is also recommended you start the Commence process with the /noagents command line parameter."))
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
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            WriteObject(GetSummaryFromLog(changeLog), true);
            WriteVerbose($"See '{logFile}' for details.");
        }

        private Dictionary<string, Dictionary<char, string>> rules;
        private void ClearControlCharactersFromCategory(string categoryName, bool doCommit)
        {
            using (var db = new CommenceDatabase())
            using (var cur = db.GetCursor(categoryName, CmcCursorType.Category, CmcOptionFlags.UseThids))
            {
                rules = GetReplacementRulesForCategory(this.CategoryName);
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
                        // explicitly close rowset, even though it will be closed automatically.
                        // it is just a safety precaution to prevent Commence 
                        // running out of resources before GC kicks in.
                        ers.Close(); 
                    }
                    if (rows % 100 == 0)
                    {
                        WriteProgress(new ProgressRecord(0, $"{rows} of {totalRows} ({this.CategoryName})", $"Commence rows processed."));
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
                    if (!oldValue.Equals(newValue)) // Equals is pretty fast and probably the best choice
                    {
                        var result = ers.ModifyRow(row, column, newValue, CmcOptionFlags.Default); // ModifyRow will truncate strings.
                        retval = true;
                        // add new result to temporary changeset
                        changeResults.Add(new FieldModification(
                            CategoryName = this.CategoryName,
                            // notice we pass rowValues[0] as the itemname
                            // that is because the Name field is always the first field to be returned from Commence,
                            // and it is also always present in our test.
                            rowValues[0], // itemname
                            rules.Keys.ElementAt(column), // fieldname
                            oldValue,
                            newValue,
                            result)); ;
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

        private Dictionary<string, Dictionary<char, string>> GetReplacementRulesForCategory(string categoryName)
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
        /// If no rules are specified, the field will not be processed.
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

        // hot path
        private string ReplaceControlCharacters(string str, Dictionary<char, string> replacementString)
        {
            // some speed optimizations: cast to chararray and loop string just once
            char[] c = str.ToCharArray();
            int length = c.Length;

            // in the majority of cases, there will be no control characters at all
            // in that case, we do not have to bother with the stringbuilder stuff
            bool changeNeeded = false;
            for (int i = 0; i < length; i++)
            {
                if (c[i] < 31) // is there a control character?
                {
                    changeNeeded = true;
                    break;
                }
            }
            // no changes needed, just return original string
            if (!changeNeeded) return str;

            // if we haven't returned by now, there are control characters to process
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                // keep 'normal' characters
                if (c[i] > 31)
                {
                    sb.Append(c[i]);
                    continue;
                }
                // ignore all except specified control characters
                // note that we do not look for \r\n combinations,
                // they may be unwanted.
                // we let the replacement rules deal with how to process them
                // we simply keep appending to stringbuilder here, even if it's an empty string we append
                if (c[i] == 13 || c[i] == 9 || c[i] == 10)
                {
                    sb.Append(replacementString[c[i]]); // we assume the dictionary entry to be there
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

        private IEnumerable<PSObject> GetSummaryFromLog(ChangeLog log)
        {
            if (!log.ModifiedRows.Any())
            {
                yield return new PSObject("No unexpected control characters found.");
                yield break;
            }
            // flatten and group FieldModification objects
            var groups = log.ModifiedRows.Values.SelectMany(d => d).GroupBy(p => p.CategoryName);
            foreach (var g in groups)
            {
                // LINQ magic. I will not remember how I did this but it gets me there
                int affectedRowCount = log.ModifiedRows.Keys
                    .Count(w => log.ModifiedRows[w].Any(a => a.CategoryName.Equals(g.Key)));
                var o = new PSObject();
                o.Members.Add(new PSNoteProperty("Category", g.Key));
                o.Members.Add(new PSNoteProperty("Affected rows", affectedRowCount));
                o.Members.Add(new PSNoteProperty("Affected fields", g.Count()));
                yield return o;
            }
        }
    }
}
