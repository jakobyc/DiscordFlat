using DiscordFlat.DTOs.WebSockets.Events.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers.Args
{
    public class DiscordOnReadyEventArgs : EventArgs
    {
        public Ready Ready { get; set; }
    }
}
