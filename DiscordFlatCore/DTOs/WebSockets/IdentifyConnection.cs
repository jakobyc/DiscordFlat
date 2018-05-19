using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets
{
    [DataContract]
    public class IdentifyConnection
    {
        [DataMember(Name = "$os")]
        public string OperatingSystem { get; set; }
        [DataMember(Name = "$browser")]
        public string Browser { get; set; }
        [DataMember(Name = "$device")]
        public string Device { get; set; }
    }
}
