using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public class RequestGuildMembers : GatewayObject
    {
        public RequestGuildMembers()
        {
            OpCode = (int)OpCodes.RequestGuildMembers;
        }
        /*[DataMember(Name = "op")]
        public int? OpCode { get => (int)OpCodes.RequestGuildMembers; set { } }*/

        [DataMember(Name = "d")]
        public GuildMembersRequest EventData { get; set; }
    }
}
