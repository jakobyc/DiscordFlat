using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Heartbeats
{
    [DataContract]
    public class Heartbeat
    {
        [DataMember(Name = "op")]
        public int OpCode { get => (int)OpCodes.Heartbeat; set { } }

        /// <summary>
        /// Set to the last sequence number received, or null if no sequence number has been received.
        /// </summary>
        [DataMember(Name = "d")]
        public string EventData { get; set; }
    }
}
