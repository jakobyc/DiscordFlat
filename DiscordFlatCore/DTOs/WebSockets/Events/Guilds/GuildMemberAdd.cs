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
    public class GuildMemberAdd : GatewayObject
    {
        [DataMember(Name = "d")]
        public GuildMemberAddFields EventData { get; set; }
    }
}
