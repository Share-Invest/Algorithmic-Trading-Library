using Microsoft.AspNetCore.SignalR.Client;

namespace ShareInvest.Infrastructure;

public interface ISocketClient<T>
{
    HubConnection Hub
    {
        get;
    }
    event EventHandler<T> Send;
}