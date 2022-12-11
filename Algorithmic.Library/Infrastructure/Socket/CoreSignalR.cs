using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

using ShareInvest.Observers;
using ShareInvest.Observers.OpenAPI;
using ShareInvest.Observers.Socket;

using System.Diagnostics;

namespace ShareInvest.Infrastructure.Socket;

public class CoreSignalR : ISocketClient<MessageEventArgs>
{
    public event EventHandler<MessageEventArgs>? Send;

    public HubConnection Hub
    {
        get;
    }
    public CoreSignalR(string url)
    {
        Hub = new HubConnectionBuilder()

            .WithUrl(url, o =>
            {
                o.AccessTokenProvider = async () =>
                {


                    return await Task.FromResult(string.Empty);
                };
            })
            .ConfigureLogging(o =>
            {
                o.AddDebug();
                o.SetMinimumLevel(LogLevel.Debug);
            })
            .WithAutomaticReconnect(new TimeSpan[]
            {
                TimeSpan.Zero,
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(0xA),
                TimeSpan.FromSeconds(0x20),
                TimeSpan.FromSeconds(0x5A)
            })
            .Build();

        OnConnection();
    }
    protected IDisposable On(string name)
    {
        return Hub.On<string, string>(name, (key, data) =>
        {
            Send?.Invoke(this,
                         new RealMessageEventArgs(key, data));
        });
    }
    void OnConnection()
    {
        Hub.ServerTimeout = TimeSpan.FromSeconds(60);
        Hub.HandshakeTimeout = TimeSpan.FromSeconds(30);

        Hub.Closed += async e =>
        {
            Send?.Invoke(this, new SignalEventArgs(Hub.State));
#if DEBUG
            Debug.WriteLine(e?.Message);
#endif
            await Task.CompletedTask;
        };
        Hub.Reconnecting += async e =>
        {
            Send?.Invoke(this, new SignalEventArgs(Hub.State));
#if DEBUG
            Debug.WriteLine(e?.Message);
#endif
            await Task.CompletedTask;
        };
    }
}