using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Heartbeats
{
    /// <summary>
    /// Heartbeat Acknowledgement
    /// </summary>
    [DataContract]
    public class HeartbeatResponse
    {
        /// <summary>
        /// Server should return OpCode 11
        /// </summary>
        [DataMember(Name = "op")]
        public string OpCode { get; set; }
    }
}
