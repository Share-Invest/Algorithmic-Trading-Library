namespace ShareInvest.Models.OpenAPI.Observe;

public class AxMessageEventArgs : EventArgs
{
    public string? Title
    {
        get;
    }
    public string? Code
    {
        get;
    }
    public string? Screen
    {
        get;
    }
    public AxMessageEventArgs(string? title,
                              string? code,
                              string? screen)
    {
        Title = title;
        Code = code;
        Screen = screen;
    }
}