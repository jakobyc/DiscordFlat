using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.Guilds
{
    [DataContract]
    public class GuildRole
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "color")]
        public int Color { get; set; }
        [DataMember(Name = "position")]
        public int Position { get; set; }
        [DataMember(Name = "permissions")]
        public int Permissions { get; set; }

        [DataMember(Name = "hoist")]
        public bool Hoist { get; set; }
        [DataMember(Name = "managed")]
        public bool Managed { get; set; }
        [DataMember(Name = "mentionable")]
        public bool Mentionable { get; set; }
    }
}
