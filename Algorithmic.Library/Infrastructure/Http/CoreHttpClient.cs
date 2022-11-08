using ShareInvest.Properties;

using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;

namespace ShareInvest.Infrastructure.Http;

public class CoreHttpClient : HttpClient, ICoreClient
{
    public async Task PostAsync<T>(string route, T param) where T : class
    {
        var url = string.Concat(Resources.URL, Resources.KIWOOM, route);

        using (var res = await this.PostAsJsonAsync(url, param, cts.Token))

            if (HttpStatusCode.OK != res.StatusCode)
            {
#if DEBUG
                Debug.WriteLine(res.StatusCode);
#endif
            }
    }
    readonly CancellationTokenSource cts = new();
}