using DiscordFlat.DTOs.WebSockets.Events.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers.Args
{
    public class DiscordOnPresenceUpdateEventArgs : EventArgs
    {
        public Presence Presence { get; set; }
    }
}
