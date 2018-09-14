using DiscordFlatCore.DTOs.WebSockets.Events.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.WebSockets.EventHandlers.Args
{
    public class DiscordOnGuildMemberRemoveEventArgs : EventArgs
    {
        public GuildMemberRemoveFields GuildMember { get; set; }
    }
}
