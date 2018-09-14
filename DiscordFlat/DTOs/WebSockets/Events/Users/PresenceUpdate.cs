using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DiscordFlatCore.DTOs.WebSockets.Events.Users
{
    [DataContract]
    public class PresenceUpdate : GatewayObject
    {
        [DataMember(Name = "d")]
        public Presence EventData { get; set; }
    }
}
