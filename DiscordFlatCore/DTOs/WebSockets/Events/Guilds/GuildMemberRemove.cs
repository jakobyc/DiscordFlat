using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets.Events.Guilds
{
    [DataContract]
    public class GuildMemberRemove : GatewayObject
    {
        [DataMember(Name = "d")]
        public GuildMemberRemoveFields EventData { get; set; }
    }
}
