using DiscordFlat.DTOs.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Events.Guilds
{
    [DataContract]
    public class GuildCreate : GatewayObject
    {
        [DataMember(Name = "d")]
        public Guild EventData { get; set; }
    }
}
