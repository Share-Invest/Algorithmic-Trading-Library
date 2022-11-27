namespace ShareInvest.Infrastructure;

public interface IHubs
{
    Task GatherCluesToPrioritize(int count);

    Task TransmitConclusionInformation(string key, string data);
}