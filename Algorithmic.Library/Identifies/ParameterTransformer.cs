using System.Text.RegularExpressions;

namespace ShareInvest.Identifies;

public static class ParameterTransformer
{
    public static string TransformOutbound(string route)
    {
        return Regex.Replace(route,
                             "([a-z])([A-Z])",
                             "$1-$2",
                             RegexOptions.CultureInvariant,
                             TimeSpan.FromMilliseconds(0x64)).ToLowerInvariant();
    }
}