using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Events.Connections
{
    [DataContract]
    public class Trace
    {
        [DataMember(Name = "_trace")]
        public List<string> TraceList { get; set; }
    }
}
