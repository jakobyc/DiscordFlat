using DiscordFlat.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.Channels
{
    [DataContract]
    public class Channel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "guild_id")]
        public string GuildId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "topic")]
        public string Topic { get; set; }
        [DataMember(Name = "last_message_id")]
        public string LastMessageId { get; set; }
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
        [DataMember(Name = "owner_id")]
        public string OwnerId { get; set; }
        [DataMember(Name = "application_id")]
        public string ApplicationId { get; set; }
        [DataMember(Name = "parent_id")]
        public string ParentId { get; set; }
        [DataMember(Name = "last_pin_timestamp")]
        public string LastPinTimestamp { get; set; }

        [DataMember(Name = "type")]
        public int Type { get; set; }
        [DataMember(Name = "position")]
        public int Position { get; set; }
        [DataMember(Name = "bitrate")]
        public int Bitrate { get; set; }
        [DataMember(Name = "user_limit")]
        public int UserLimit { get; set; }

        [DataMember(Name = "nsfw")]
        public bool Nsfw { get; set; }

        [DataMember(Name = "recipients")]
        public DiscordUsers Recipients { get; set; }

        // TODO: Add overwrite and overwrites DTO's
        [DataMember(Name = "permission_overwrites")]
        public object PermissionOverwrites { get; set; }
    }
}
