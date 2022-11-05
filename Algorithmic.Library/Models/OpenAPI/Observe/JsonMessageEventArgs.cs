using Newtonsoft.Json;

using ShareInvest.Models.OpenAPI.Response;

namespace ShareInvest.Models.OpenAPI.Observe;

public class JsonMessageEventArgs : MessageEventArgs
{
    public object? Convey
    {
        get;
    }
    public JsonMessageEventArgs(TR? tr, string json) =>

        Convey = tr switch
        {
            Request.OPTKWFID => JsonConvert.DeserializeObject<OPTKWFID>(json),

            _ => throw new ArgumentNullException(json)
        };
}