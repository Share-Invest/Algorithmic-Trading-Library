using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace ShareInvest
{
    public static class Status
    {
        public static string Address => IsDebugging ? "" : Properties.Resources.URL;

        [SupportedOSPlatform("windows8.0")]
        public static bool IsAdministrator
        {
            get
            {
                using (var cur = WindowsIdentity.GetCurrent())
                {
                    return new WindowsPrincipal(cur).IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
        }
        public static bool IsDebugging
        {
            get; private set;
        }
        public static string GetId(string[] chaos)
        {
            var physical = string.Empty;

            foreach (var net in NetworkInterface.GetAllNetworkInterfaces())
            {
                physical = net.GetPhysicalAddress().ToString();

                if (string.IsNullOrEmpty(physical) is false && physical.Length == 0xC)
                    break;
            }
            return string.Concat(chaos[^1],
                                 physical[0..3],
                                 chaos[^2],
                                 physical[3..6],
                                 chaos[^3],
                                 physical[6..9],
                                 chaos[^4],
                                 physical[9..0xC],
                                 chaos[^5]);
        }
        [Conditional("DEBUG")]
        public static void SetDebug()
        {
            IsDebugging = true;

            Debug.WriteLine(nameof(SetDebug));
        }
    }
}