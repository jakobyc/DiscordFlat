using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.Guilds
{
    [DataContract]
    public class ModifyGuildMember
    {
        [DataMember(Name = "nick")]
        public string Nickname { get; set; }
        [DataMember(Name = "channel_id")]
        public string ChannelId { get; set; }

        [DataMember(Name = "mute")]
        public bool Mute { get; set; }
        [DataMember(Name = "deaf")]
        public bool Deaf { get; set; }

        [DataMember(Name = "roles")]
        public List<string> Roles { get; set; }
    }
}
