namespace VRCStalker
{
    public class Config
    {
        public bool Location { get; set; }
        public bool Status { get; set; }
        public bool Remember { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Config(bool location, bool status, bool remember = false, string user = "NULL", string pass = "NULL")
        {
            Location = location;
            Status = status;
            Remember = remember;
            Username = user;
            Password = pass;
        }
    }
}
