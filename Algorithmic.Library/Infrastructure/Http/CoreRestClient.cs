using RestSharp;

using ShareInvest.Properties;

using System.Diagnostics;
using System.Net;

namespace ShareInvest.Infrastructure.Http;

public class CoreRestClient : RestClient, ICoreClient
{
    public async Task<object> PostAsync<T>(string route, T param) where T : class
    {
        var transformer = Identifies.Parameter.TransformOutbound(route);

        var request = new RestRequest($"{Resources.KIWOOM}/{transformer}", Method.POST);

        request.AddJsonBody(param);

        var res = await ExecuteAsync(request, cancellationTokenSource.Token);

        if (HttpStatusCode.OK != res.StatusCode)
        {
#if DEBUG
            Debug.WriteLine(res.StatusCode);
#endif
        }
        return res.Content;
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