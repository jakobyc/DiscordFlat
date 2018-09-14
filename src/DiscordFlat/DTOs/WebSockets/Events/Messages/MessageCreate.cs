using DiscordFlat.DTOs.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Events.Messages
{
    [DataContract]
    public class MessageCreate : GatewayObject
    {
        [DataMember(Name = "d")]
        public Message EventData { get; set; }
    }
}
