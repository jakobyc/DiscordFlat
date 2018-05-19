using DiscordFlatCore.DTOs.WebSockets.Events.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.WebSockets.EventHandlers.Args
{
    public class DiscordOnResumeEventArgs : EventArgs
    {
        public Trace Trace { get; set; }
    }
}
