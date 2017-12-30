using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    public enum OpCodes
    {
        Dispatch = 0,
        Heartbeat = 1,
        Identify = 2,
        StatusUpdate = 3,
        VoiceStateUpdate = 4,
        VoiceServerPing = 5,
        Resume = 6,
        Reconnect = 7,
        RequestGuildMembers = 8,
        InvalidSession = 9,
        Hello = 10,
        HeartbeatAcknowledged = 11
    };

    [DataContract]
    public class GatewayObject
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
