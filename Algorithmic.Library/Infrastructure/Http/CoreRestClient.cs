using Newtonsoft.Json;

using RestSharp;

using ShareInvest.Models.OpenAPI.Response;
using ShareInvest.Properties;

using System.Diagnostics;
using System.Net;

namespace ShareInvest.Infrastructure.Http;

public class CoreRestClient : RestClient, ICoreClient
{
    public async Task PostAsync<T>(string route, T param) where T : class
    {
        var request = new RestRequest($"{Resources.API}/{route}", Method.POST);

        object? ctor = param;

        if (param is string json)
            ctor = route switch
            {
                nameof(OPTKWFID) => JsonConvert.DeserializeObject<OPTKWFID>(json),
                _ => null
            };
        if (ctor is null)
            return;

        request.AddJsonBody(ctor);

        var res = await ExecuteAsync(request, cancellationTokenSource.Token);

        if (HttpStatusCode.OK != res.StatusCode)
            Debug.WriteLine(res.StatusCode);
    }
    public CoreRestClient() : base(Resources.URL)
    {
        Timeout = -1;
        cancellationTokenSource = new CancellationTokenSource();
    }
    public CoreRestClient(string url) : base(url)
    {
        Timeout = -1;
        cancellationTokenSource = new CancellationTokenSource();
    }
    readonly CancellationTokenSource cancellationTokenSource;
}