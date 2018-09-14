using DiscordFlat.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Events.Users
{
    [DataContract]
    public class Presence
    {
        [DataMember(Name = "guild_id")]
        public string GuildId { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "user")]
        public DiscordUser User { get; set; }

        [DataMember(Name = "roles")]
        public List<string> Roles { get; set; }
    }
}
