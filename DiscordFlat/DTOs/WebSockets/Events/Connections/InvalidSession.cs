using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets.Events.Connections
{
    [DataContract]
    public class InvalidSession : GatewayObject
    {
        [DataMember(Name = "d")]
        public bool Resumable { get; set; }
    }
}
