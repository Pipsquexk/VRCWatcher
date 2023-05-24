namespace VRCWatcher
{
    public class SocketInfo
    {
        public string type { get; set; }
        public string content { get; set; }
    }

    public class OfflineContent
    {
        public string userId { get; set; }
    }

    public class LocationContent
    {
        public string userId { get; set; }
        public SocketUser user { get; set; }
        public string location { get; set; }
        public string instance { get; set; }
        public World world { get; set; }
    }

    public class World
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class SocketUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string userIcon { get; set; }
        public string bio { get; set; }
        public string[] bioLinks { get; set; }
        public string[] pastDisplayNames { get; set; }
        public bool hasEmail { get; set; }
        public bool hasPendingEmail { get; set; }
        public string email { get; set; }
        public string obfuscatedEmail { get; set; }
        public string obfuscatedPendingEmail { get; set; }
        public bool emailVerified { get; set; }
        public bool hasBirthday { get; set; }
        public bool unsubscribe { get; set; }
        public string[] friends { get; set; }
    }

}
