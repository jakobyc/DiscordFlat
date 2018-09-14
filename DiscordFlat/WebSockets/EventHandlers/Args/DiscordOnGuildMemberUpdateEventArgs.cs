using DiscordFlatCore.DTOs.WebSockets.Events.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.WebSockets.EventHandlers.Args
{
    public class DiscordOnGuildMemberUpdateEventArgs : EventArgs
    {
        public GuildMemberUpdateFields GuildMember { get; set; }
    }
}
