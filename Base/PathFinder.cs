using System;
using System.IO;
using Vovin.CmcLibNet.Database;

namespace PSCommenceModules.Base
{
    internal static class PathFinder
    {
        private static string GetDatabaseName()
        {
            using (ICommenceDatabase db = new CommenceDatabase()) {
                return db.Name;
            }
        }
        private static FileInfo GetLogFile()
        {
            string logFile = "active.log";
            using (ICommenceDatabase db = new CommenceDatabase()) {
                return new FileInfo(db.Path + Path.DirectorySeparatorChar + logFile);
            }
        }

        private static FileInfo GetIniFile()
        {
            string iniFile = "data.ini";
            using (ICommenceDatabase db = new CommenceDatabase()) {
                return new FileInfo(db.Path + Path.DirectorySeparatorChar + iniFile);
            }
        }

        private static DirectoryInfo GetDatabaseDirectory()
        {
            using (ICommenceDatabase db = new CommenceDatabase()) {
                return new DirectoryInfo(db.Path);
            }
        }

        internal static string DatabaseName => GetDatabaseName();
        internal static FileInfo LogFile => GetLogFile();
        internal static FileInfo IniFile => GetIniFile();
        internal static DirectoryInfo DatabaseDirectory => GetDatabaseDirectory();
    }
}