namespace ShareInvest.Infrastructure;

public interface ICoreClient
{
    Task<object> PostAsync<T>(string route, T param) where T : class;
}