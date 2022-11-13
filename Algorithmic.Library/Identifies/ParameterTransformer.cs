using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ShareInvest.Models;
using ShareInvest.Models.OpenAPI.Response;
using ShareInvest.Properties;

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
    public static AccountBook? DeserializeOPW00004(string json)
    {
        var jEnumerable = JObject.Parse(json).AsJEnumerable();

        if (jEnumerable.Any(o => Resources.CODE.Equals(o.Path)))
        {
            return JsonConvert.DeserializeObject<BalanceOPW00004>(json);
        }
        return JsonConvert.DeserializeObject<AccountOPW00004>(json);
    }
    public static AccountBook? DeserializeOPW00005(string json)
    {
        var jEnumerable = JObject.Parse(json).AsJEnumerable();

        if (jEnumerable.Any(o => Resources.CODENUMBER.Equals(o.Path)))
        {
            return JsonConvert.DeserializeObject<BalanceOPW00005>(json);
        }
        return JsonConvert.DeserializeObject<AccountOPW00005>(json);
    }
}