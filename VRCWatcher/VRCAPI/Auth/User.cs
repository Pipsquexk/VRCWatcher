using System;

namespace VRCWatcher.VRCAPI.Auth
{
    public class PastDisplayName
    {
        public string displayName { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string userIcon { get; set; }
        public string bio { get; set; }
        public object bioLinks { get; set; }
        public string profilePicOverride { get; set; }
        public string statusDescription { get; set; }
        public PastDisplayName[] pastDisplayNames { get; set; }
        public bool hasEmail { get; set; }
        public bool hasPendingEmail { get; set; }
        public string obfuscatedEmail { get; set; }
        public string obfuscatedPendingEmail { get; set; }
        public bool emailVerified { get; set; }
        public bool hasBirthday { get; set; }
        public bool unsubscribe { get; set; }
        public string[] statusHistory { get; set; }
        public bool statusFirstTime { get; set; }
        public string[] friends { get; set; }
        public object[] friendGroupNames { get; set; }
        public string currentAvatarImageUrl { get; set; }
        public string currentAvatarThumbnailImageUrl { get; set; }
        public string currentAvatar { get; set; }
        public string currentAvatarAssetUrl { get; set; }
        public string fallbackAvatar { get; set; }
        public object accountDeletionDate { get; set; }
        public int acceptedTOSVersion { get; set; }
        public string steamId { get; set; }
        public object steamDetails { get; set; }
        public string oculusId { get; set; }
        public bool hasLoggedInFromClient { get; set; }
        public string homeLocation { get; set; }
        public bool twoFactorAuthEnabled { get; set; }
        public object twoFactorAuthEnabledDate { get; set; }
        public string state { get; set; }
        public string[] tags { get; set; }
        public string developerType { get; set; }
        public DateTime last_login { get; set; }
        public string last_platform { get; set; }
        public bool allowAvatarCopying { get; set; }
        public string status { get; set; }
        public string date_joined { get; set; }
        public bool isFriend { get; set; }
        public string friendKey { get; set; }
        public DateTime last_activity { get; set; }
        public string[] onlineFriends { get; set; }
        public string[] activeFriends { get; set; }
        public string[] offlineFriends { get; set; }
    }

    public class OtherUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string userIcon { get; set; }
        public string bio { get; set; }
        public object[] bioLinks { get; set; }
        public string profilePicOverride { get; set; }
        public string statusDescription { get; set; }
        public string currentAvatarImageUrl { get; set; }
        public string currentAvatarThumbnailImageUrl { get; set; }
        public string state { get; set; }
        public string[] tags { get; set; }
        public string developerType { get; set; }
        public DateTime last_login { get; set; }
        public string last_platform { get; set; }
        public bool allowAvatarCopying { get; set; }
        public string status { get; set; }
        public string date_joined { get; set; }
        public bool isFriend { get; set; }
        public string friendKey { get; set; }
        public DateTime last_activity { get; set; }
        public string worldId { get; set; }
        public string instanceId { get; set; }
        public string location { get; set; }

    }

    public class OtherMUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string userIcon { get; set; }
        public string bio { get; set; }
        public object[] bioLinks { get; set; }
        public string profilePicOverride { get; set; }
        public string statusDescription { get; set; }
        public PastDisplayName[] pastDisplayNames { get; set; }
        public bool hasEmail { get; set; }
        public bool hasPendingEmail { get; set; }
        public string obfuscatedEmail { get; set; }
        public string obfuscatedPendingEmail { get; set; }
        public bool emailVerified { get; set; }
        public bool hasBirthday { get; set; }
        public bool unsubscribe { get; set; }
        public string[] statusHistory { get; set; }
        public bool statusFirstTime { get; set; }
        public string[] friends { get; set; }
        public object[] friendGroupNames { get; set; }
        public string currentAvatarImageUrl { get; set; }
        public string currentAvatarThumbnailImageUrl { get; set; }
        public string currentAvatar { get; set; }
        public string currentAvatarAssetUrl { get; set; }
        public string fallbackAvatar { get; set; }
        public object accountDeletionDate { get; set; }
        public int acceptedTOSVersion { get; set; }
        public string steamId { get; set; }
        public object steamDetails { get; set; }
        public string oculusId { get; set; }
        public bool hasLoggedInFromClient { get; set; }
        public string homeLocation { get; set; }
        public bool twoFactorAuthEnabled { get; set; }
        public object twoFactorAuthEnabledDate { get; set; }
        public string state { get; set; }
        public string[] tags { get; set; }
        public string developerType { get; set; }
        public DateTime last_login { get; set; }
        public string last_platform { get; set; }
        public bool allowAvatarCopying { get; set; }
        public string status { get; set; }
        public string date_joined { get; set; }
        public bool isFriend { get; set; }
        public string friendKey { get; set; }
        public DateTime last_activity { get; set; }
        public string worldId { get; set; }
        public string instanceId { get; set; }
        public string location { get; set; }
    }
}
