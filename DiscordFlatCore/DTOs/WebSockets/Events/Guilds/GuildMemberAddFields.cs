using DiscordFlatCore.DTOs.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets.Events.Guilds
{
    [DataContract]
    public class GuildMemberAddFields : GuildMember
    {
        [DataMember(Name = "guild_id")]
        public string GuildId { get; set; }
    }
}
