using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.WebSockets.Events.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.Caches
{
    public class DiscordWebSocketCache
    {
        public TokenResponse Token { get; set; }
        public ReadyResponse ReadyResponse { internal get; set; }
    }
}
