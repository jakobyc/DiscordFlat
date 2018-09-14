﻿using DiscordFlatCore.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets.Events.Guilds
{
    [DataContract]
    public class GuildMemberUpdateFields
    {
        [DataMember(Name = "guild_id")]
        public string GuildId { get; set; }
        [DataMember(Name = "nick")]
        public string Nickname { get; set; }

        [DataMember(Name = "user")]
        public DiscordUser User { get; set; }

        [DataMember(Name = "roles")]
        public List<string> Roles { get; set; }
    }
}
