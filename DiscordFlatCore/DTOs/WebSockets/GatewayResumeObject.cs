using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets
{
    [DataContract]
    public class GatewayResumeObject : GatewayObject
    {
        public GatewayResumeObject()
        {
            OpCode = (int)OpCodes.Resume;
        }

        [DataMember(Name = "d")]
        public GatewayResume Resume { get; set; }
    }
}
