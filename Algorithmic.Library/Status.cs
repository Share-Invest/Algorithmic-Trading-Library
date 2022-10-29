using System.Diagnostics;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace ShareInvest
{
    public static class Status
    {
        public static string Address => IsDebugging ? "" : url;

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
        [Conditional("DEBUG")]
        public static void SetDebug()
        {
            IsDebugging = true;

            Debug.WriteLine(nameof(SetDebug));
        }
        const string url = "https://coreapi.shareinvest.net";
    }
}