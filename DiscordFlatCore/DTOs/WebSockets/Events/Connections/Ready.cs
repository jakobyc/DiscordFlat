using DiscordFlatCore.DTOs.Channels;
using DiscordFlatCore.DTOs.Guilds;
using DiscordFlatCore.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets.Events.Connections
{
    [DataContract]
    public class Ready
    {
        /// <summary>
        /// Used for resuming connections.
        /// </summary>
        [DataMember(Name = "session_id")]
        public string SessionId { get; set; }

        /// <summary>
        /// Used for debugging - the guilds the user is in.
        /// </summary>
        [DataMember(Name = "_trace")]
        public List<string> Trace { get; set; }

        /// <summary>
        /// Gateway protocol version.
        /// </summary>
        [DataMember(Name = "v")]
        public int Version { get; set; }

        [DataMember(Name = "user")]
        public DiscordUser User { get; set; }

        // TODO: Add DM channel object
        [DataMember(Name = "private_channels")]
        public ChannelList PrivateChannels { get; set; }

        [DataMember(Name = "guilds")]
        public UnavailableGuilds Guilds { get; set; }
    }
}
