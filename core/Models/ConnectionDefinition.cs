namespace Core.Models
{
    public class ConnectionDefinition
    {
        public string From {get;init;} = default!;
        public string To {get;init;} = default!;
        public string? Branch {get;init;}
    }
}