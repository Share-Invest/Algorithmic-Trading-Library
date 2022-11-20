using ShareInvest.Identifies;
using ShareInvest.Properties;

using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;

namespace ShareInvest.Infrastructure.Http;

public class CoreHttpClient : HttpClient, ICoreClient
{
    public async Task<object> PostAsync<T>(string route, T param) where T : class
    {
        var url = string.Concat(Resources.URL, Resources.KIWOOM, route);

        using (var res = await this.PostAsJsonAsync(url, param, cts.Token))
        {
            if (HttpStatusCode.OK != res.StatusCode)
            {
#if DEBUG
                Debug.WriteLine(res.StatusCode);
#endif
            }
            else
                return res.Content;
        }
        return string.Empty;
    }
    public Task<T?> TryGetAsync<T>(string param)
    {
        string path = string.Concat(Resources.KIWOOM,
                                    '/',
                                    ParameterTransformer.TransformOutbound(param));
        if (FirstRendering)
        {
            FirstRendering = false;

            return Task.Run(() => TryGetImplementationAsync<T>(path));
        }
        return TryGetImplementationAsync<T>(path);
    }
    public CoreHttpClient(string url)
    {
        FirstRendering = true;
        BaseAddress = new Uri(url);
    }
    async Task<T?> TryGetImplementationAsync<T>(string path)
    {
        return await this.GetFromJsonAsync<T>(path);
    }
    bool FirstRendering
    {
        get; set;
    }
    readonly CancellationTokenSource cts = new();
}