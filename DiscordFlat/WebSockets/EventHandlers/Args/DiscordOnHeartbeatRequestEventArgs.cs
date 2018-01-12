using DiscordFlat.DTOs.WebSockets.Heartbeats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers.Args
{
    public class DiscordOnHeartbeatRequestEventArgs : EventArgs
    {
        public Heartbeat Heartbeat { get; set; }
    }
}
