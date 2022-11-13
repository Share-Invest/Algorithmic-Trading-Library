namespace ShareInvest.Infrastructure;

public interface IHubs
{
    Task GatherCluesToPrioritize(int count);
}