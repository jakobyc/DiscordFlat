using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Events.Users
{
    public enum ActivityTypes
    {
        Game = 0,
        Streaming = 1,
        Listening = 2,
        Watching = 3
    }
    [DataContract]
    public class Game
    {
        public string Name { get; set; }
        /// <summary>
        /// Stream Url when Type = 1.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Activity Type
        /// </summary>
        public int Type { get; set; }

    }
}
