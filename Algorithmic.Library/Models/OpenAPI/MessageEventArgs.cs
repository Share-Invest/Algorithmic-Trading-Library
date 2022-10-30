namespace ShareInvest.Models.OpenAPI
{
    public class MessageEventArgs : EventArgs
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
        public MessageEventArgs(string? title,
                                string? code,
                                string? screen)
        {
            Title = title;
            Code = code;
            Screen = screen;
        }
    }
}