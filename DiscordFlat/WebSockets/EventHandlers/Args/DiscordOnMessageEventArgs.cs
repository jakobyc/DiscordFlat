using DiscordFlat.DTOs.Channels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers.Args
{
    public class DiscordOnMessageEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }
}
