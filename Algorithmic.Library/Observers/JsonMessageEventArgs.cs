using Newtonsoft.Json;

using ShareInvest.Identifies;
using ShareInvest.Models.OpenAPI;
using ShareInvest.Models.OpenAPI.Response;

namespace ShareInvest.Observers;

public class JsonMessageEventArgs : MessageEventArgs
{
    public object? Convey
    {
        get;
    }
    public JsonMessageEventArgs(TR? tr, string json)
    {
        Convey = tr switch
        {
            Models.OpenAPI.Request.OPW00004 =>

                ParameterTransformer.DeserializeObject(json),

            Models.OpenAPI.Request.OPTKWFID =>

                JsonConvert.DeserializeObject<OPTKWFID>(json),

            _ => throw new ArgumentNullException(json)
        };
    }
}