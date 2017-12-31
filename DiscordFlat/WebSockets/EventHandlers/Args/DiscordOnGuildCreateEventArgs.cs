using DiscordFlat.DTOs.Guilds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers.Args
{
    public class DiscordOnGuildCreateEventArgs : EventArgs
    {
        public Guild Guild { get; set; }
    }
}
