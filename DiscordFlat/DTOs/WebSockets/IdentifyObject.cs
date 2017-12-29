using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public class IdentifyObject
    {
        /// <summary>
        /// Opcode for the payload.
        /// </summary>
        [DataMember(Name = "op")]
        public int? OpCode { get; set; }

        /// <summary>
        /// Event data for the payload.
        /// </summary>
        [DataMember(Name = "d")]
        public Identify EventData { get; set; }
    }
}
