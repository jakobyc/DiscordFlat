using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public class HelloObject : GatewayObject
    {
        /// <summary>
        /// Event data for the payload.
        /// </summary>
        [DataMember(Name = "d")]
        public GatewayHello EventData { get; set; }

    }
}
