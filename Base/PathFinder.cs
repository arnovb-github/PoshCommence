using System.IO;
using Vovin.CmcLibNet.Database;

namespace PoshCommence.Base
{
    internal static class PathFinder
    {
        private static string GetDatabaseName()
        {
            using (ICommenceDatabase db = new CommenceDatabase()) {
                return db.Name;
            }
        }

        private static FileInfo GetFileInfoFromDatabasePath(string fileName)
        {
            using (ICommenceDatabase db = new CommenceDatabase()) {
                string filePath = db.Path + Path.DirectorySeparatorChar + fileName;
                if (File.Exists(filePath))
                {
                    return new FileInfo(filePath);
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }

        }
        private static DirectoryInfo GetDatabaseDirectory()
        {
            using (ICommenceDatabase db = new CommenceDatabase()) {
                return new DirectoryInfo(db.Path);
            }
        }

        internal static string DatabaseName => GetDatabaseName();
        internal static FileInfo LogFile => GetFileInfoFromDatabasePath("active.log");
        internal static FileInfo IniFile => GetFileInfoFromDatabasePath("data.ini");
        internal static DirectoryInfo DatabaseDirectory => GetDatabaseDirectory();
    }
}