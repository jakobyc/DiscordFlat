using DiscordFlat.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Events.Guilds
{
    [DataContract]
    public class GuildMemberRemoveFields
    {
        [DataMember(Name = "guild_id")]
        public string GuildId { get; set; }

        [DataMember(Name = "user")]
        public DiscordUser User { get; set; }
    }
}
