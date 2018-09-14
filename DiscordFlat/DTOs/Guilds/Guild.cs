using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Guilds
{
    [DataContract]
    public class Guild
    {
        // TODO: Add Emojis/Voice States/Members/Channels/Presences
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
        [DataMember(Name = "splash")]
        public string Splash { get; set; }
        [DataMember(Name = "owner_id")]
        public string OwnerId { get; set; }
        [DataMember(Name = "region")]
        public string Region { get; set; }
        [DataMember(Name = "afk_channel_id")]
        public string AfkChannelId { get; set; }
        [DataMember(Name = "embed_channel_id")]
        public string EmbedChannelId { get; set; }
        [DataMember(Name = "application_id")]
        public string ApplicationId { get; set; }
        [DataMember(Name = "widget_channel_id")]
        public string WidgetChannelId { get; set; }
        [DataMember(Name = "system_channel_id")]
        public string SystemChannelId { get; set; }
        [DataMember(Name = "joined_at")]
        public string JoinedAt { get; set; }

        [DataMember(Name = "owmer")]
        public bool Owner { get; set; }
        [DataMember(Name = "embed_enabled")]
        public bool EmbedEnabled { get; set; }
        [DataMember(Name = "widget_enabled")]
        public bool WidgetEnabled { get; set; }
        [DataMember(Name = "large")]
        public bool Large { get; set; }
        [DataMember(Name = "unavailable")]
        public bool Unavailable { get; set; }

        [DataMember(Name = "permissions")]
        public int Permissions { get; set; }
        [DataMember(Name = "afk_timeout")]
        public int AfkTimeout { get; set; }
        [DataMember(Name = "verification_level")]
        public int VerificationLevel { get; set; }
        [DataMember(Name = "default_message_notifications")]
        public int DefaultMessageNotifications { get; set; }
        [DataMember(Name = "explicit_content_filter")]
        public int ExplicitContentFilter { get; set; }
        [DataMember(Name = "mfa_level")]
        public int MfaLevel { get; set; }
        [DataMember(Name = "member_count")]
        public int MemberCount { get; set; }

        [DataMember(Name = "roles")]
        public GuildRoles Roles { get; set; }

        [DataMember(Name = "features")]
        public List<string> Features { get; set; }
    }
}
