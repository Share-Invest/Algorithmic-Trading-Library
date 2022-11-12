using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ShareInvest.Observers;
using ShareInvest.Observers.Socket;

using System.Diagnostics;

namespace ShareInvest.Infrastructure.Socket;

public class CoreSignalR : ISocketClient<MessageEventArgs>
{
    public event EventHandler<MessageEventArgs>? Send;

    public HubConnection Hub => hub;

    public CoreSignalR(string url)
    {
        hub = new HubConnectionBuilder()

            .AddJsonProtocol(o =>
            {

            })
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

        hub.Closed += async e =>
        {
            Send?.Invoke(this, new SignalEventArgs(hub.State));
#if DEBUG
            Debug.WriteLine(e?.Message);
#endif
            await Task.CompletedTask;
        };
        hub.Reconnecting += async e =>
        {
            Send?.Invoke(this, new SignalEventArgs(hub.State));
#if DEBUG
            Debug.WriteLine(e?.Message);
#endif
            await Task.CompletedTask;
        };
    }
    readonly HubConnection hub;
}