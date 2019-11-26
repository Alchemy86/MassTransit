namespace Trabalhos.EventsEngine.Models
{
    using System;

    public class Settings
    {
        public Rabbit Rabbit { get; set; }
    }

    public class Rabbit
    {
        public Uri Uri { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Queue { get; set; }
        public string FaultQueue { get; set; }
    }
}
