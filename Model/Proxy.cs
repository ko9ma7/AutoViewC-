namespace AutoViewWebAdsCSharp.Model

{
    public class Proxy
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string IP { get; set; }

        public string Port { get; set; }

        public bool RequireAuthenticate { get; set; } = false;

        public bool Reset { get; set; } = false;

        public Proxy() { }
        public Proxy(string ip, string port)
        {
            IP = ip;
            Port = port;
        }


        public Proxy(string ip, string port, string u, string p)
        {
            IP = ip;
            Port = port;
            Username = u;
            Password = p;
        }

    }
}
