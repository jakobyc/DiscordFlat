using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public class GatewayHello
    {
        [DataMember(Name = "heartbeat_interval")]
        public int HeartbeatInterval { get; set; }

        [DataMember(Name = "_trace")]
        public List<string> Trace { get; set; }
    }
}
