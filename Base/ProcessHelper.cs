using System.Diagnostics;
using System.Linq;

namespace PoshCommence.Base
{
    public static class ProcessHelper
    {
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