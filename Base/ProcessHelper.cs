using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PoshCommence.Base
{
    public static class ProcessHelper
    {
        // public static int GetProcessCountInCurrentSession(string processName)
        // {
        //     Process[] ps = Process.GetProcessesByName(processName);
        //     int currentSessionID = Process.GetCurrentProcess().SessionId;
        //     Process[] sameAsthisSession = (from c in ps where c.SessionId == currentSessionID select c).ToArray();
        //     return sameAsthisSession.Length;
        // }

        public static Process GetProcessFromTitle(string processName, string windowTitle)
        {
            Process[] ps = Process.GetProcessesByName(processName).ToArray();
            foreach (Process p in ps)
            {
                if (p.MainWindowTitle.Contains(windowTitle))
                {
                    return p;
                }
            }
            return null;
        }
    }
}