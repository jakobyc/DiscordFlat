using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets.Events.Messages
{
    [DataContract]
    public class TypingStartResponse : GatewayObject
    {
        [DataMember(Name = "d")]
        public TypingStart EventData { get; set; }
    }
}
