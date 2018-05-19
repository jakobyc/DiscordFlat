using DiscordFlatCore.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Emojis
{
    [DataContract]
    public class Emoji
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "user")]
        public DiscordUser User { get; set; }

        [DataMember(Name = "roles")]
        public List<string> RoleIds { get; set; }

        [DataMember(Name = "require_colons")]
        public bool RequireColons { get; set; }
        [DataMember(Name = "managed")]
        public bool Managed { get; set; }
    }
}
