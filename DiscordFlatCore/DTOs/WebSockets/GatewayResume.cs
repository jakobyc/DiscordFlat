using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets
{
    [DataContract]
    public class GatewayResume
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
        [DataMember(Name = "session_id")]
        public string SessionId { get; set; }

        [DataMember(Name = "seq")]
        public int? SequenceNumber { get; set; }
    }
}
