using Newtonsoft.Json.Linq;

namespace ShareInvest.Infrastructure;

public interface ICoreClient
{
    Task<object> PostAsync<T>(string route, T param) where T : class;

    Task<object> GetAsync(string route, JToken token);
}