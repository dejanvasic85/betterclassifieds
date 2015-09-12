using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace Paramount.Betterclassifieds.Console
{
    public static class SingleInstance
    {
        public static bool IsRunning(string processName)
        {
            IEnumerable<string> argsList = GetCommandLines("OfflineTaskManager") ?? new List<string>();

            return argsList.Any(process => process.IndexOf(processName, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        /// <summary>
        /// Using WMI to fetch the command line that started all instances of a process
        /// </summary>
        private static IEnumerable<string> GetCommandLines(string processName)
        {
            string wmiQuery = string.Format("select CommandLine from Win32_Process where Name like '%{0}%'", processName);

            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery))
            using (ManagementObjectCollection retObjectCollection = searcher.Get())
            {
                return retObjectCollection.OfType<ManagementObject>().Select(item => (string)item["CommandLine"]).ToList();
            }
        }
    }
}