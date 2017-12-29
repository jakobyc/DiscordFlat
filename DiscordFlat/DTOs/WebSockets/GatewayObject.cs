using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public abstract class GatewayObject
    {
        /// <summary>
        /// Opcode for the payload.
        /// </summary>
        [DataMember(Name = "op")]
        public int? OpCode { get; set; }

        /// <summary>
        /// Sequence number, used for resuming sessions and heartbeats.
        /// </summary>
        [DataMember(Name = "s")]
        public int? SequenceNumber { get; set; }

        /// <summary>
        /// Event name for the payload.
        /// </summary>
        [DataMember(Name = "t")]
        public string EventName { get; set; }
    }
}
