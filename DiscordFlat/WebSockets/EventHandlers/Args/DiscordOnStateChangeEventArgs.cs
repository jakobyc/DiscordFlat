using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers.Args
{
    public class DiscordOnStateChangeEventArgs
    {
        public WebSocketState State { get; set; }
    }
}
