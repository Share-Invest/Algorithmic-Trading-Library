﻿using Newtonsoft.Json.Linq;

using ShareInvest.Identifies;
using ShareInvest.Properties;

using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;

namespace ShareInvest.Infrastructure.Http;

public class CoreHttpClient : HttpClient, ICoreClient
{
    public Task<object> GetAsync(string route, JToken token)
    {
        throw new NotImplementedException();
    }
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
    public CoreHttpClient(string url)
    {
        FirstRendering = true;
        BaseAddress = new Uri(url);
    }
    protected Task<T?> TryGetAsync<T>(string param)
    {
        string path = string.Concat(Resources.KIWOOM,
                                    '/',
                                    Parameter.TransformOutbound(param));
        if (FirstRendering)
        {
            FirstRendering = false;

            return Task.Run(() => TryGetImplementationAsync<T>(path));
        }
        return TryGetImplementationAsync<T>(path);
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