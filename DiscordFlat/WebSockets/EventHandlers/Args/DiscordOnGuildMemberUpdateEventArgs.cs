using DiscordFlat.DTOs.WebSockets.Events.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers.Args
{
    public class DiscordOnGuildMemberUpdateEventArgs : EventArgs
    {
        public GuildMemberUpdateFields GuildMember { get; set; }
    }
}
