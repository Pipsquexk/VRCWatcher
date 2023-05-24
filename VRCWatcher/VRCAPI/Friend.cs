using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace VRCWatcher.VRCAPI
{
    public class Friend
    {
        public string id { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string bio { get; set; }
        public string currentAvatarImageUrl { get; set; }
        public string currentAvatarThumbnailImageUrl { get; set; }
        public string fallbackAvatar { get; set; }
        public string userIcon { get; set; }
        public string profilePicOverride { get; set; }
        public string last_platform { get; set; }
        public string[] tags { get; set; }
        public string developerType { get; set; }
        public string status { get; set; }
        public string statusDescription { get; set; }
        public string friendKey { get; set; }
        public DateTime last_login { get; set; }
        public bool isFriend { get; set; }
        public string location { get; set; }
        public string travelingToLocation { get; set; }
    }
}
