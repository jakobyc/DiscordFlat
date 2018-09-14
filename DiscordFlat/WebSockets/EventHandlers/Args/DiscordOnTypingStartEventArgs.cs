using DiscordFlat.DTOs.WebSockets.Events.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers.Args
{
    public class DiscordOnTypingStartEventArgs : EventArgs
    {
        public TypingStart TypingStart { get; set; }
    }
}
