using System.Diagnostics;

namespace ShareInvest
{
    public static class Status
    {
        public static string Address => IsDebugging ? "" : url;

        public static bool IsDebugging
        {
            get; private set;
        }
        public static void SetDebug()
        {
            IsDebugging = true;

            Debug.WriteLine(nameof(SetDebug));
        }
        const string url = "https://coreapi.shareinvest.net";
    }
}