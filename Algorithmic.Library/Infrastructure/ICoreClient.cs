namespace ShareInvest.Infrastructure;

public interface ICoreClient
{
    Task PostAsync<T>(string route, T param) where T : class;
}