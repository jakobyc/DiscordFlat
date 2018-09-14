using DiscordFlatCore.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Guilds
{
    [DataContract]
    public class GuildMember
    {
        [DataMember(Name = "user")]
        public DiscordUser User { get; set; }

        [DataMember(Name = "nick")]
        public string Nick { get; set; }

        [DataMember(Name = "roles")]
        public List<string> Roles { get; set; }

        [DataMember(Name = "joined_at")]
        public string JoinedAt { get; set; }

        [DataMember(Name = "deaf")]
        public bool Deaf { get; set; }
        [DataMember(Name = "mute")]
        public bool Mute { get; set; }
    }
}
