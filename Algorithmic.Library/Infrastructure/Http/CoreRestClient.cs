using RestSharp;

using ShareInvest.Identifies;
using ShareInvest.Properties;

using System.Diagnostics;
using System.Net;

namespace ShareInvest.Infrastructure.Http;

public class CoreRestClient : RestClient, ICoreClient
{
    public async Task PostAsync<T>(string route, T param) where T : class
    {
        var transformer = ParameterTransformer.TransformOutbound(route);

        var request = new RestRequest($"{Resources.API}/{transformer}", Method.POST);

        request.AddJsonBody(param);

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