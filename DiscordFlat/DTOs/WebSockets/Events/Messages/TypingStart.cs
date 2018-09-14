using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.WebSockets.Events.Messages
{
    [DataContract]
    public class TypingStart
    {
        [DataMember(Name = "channel_id")]
        public string ChannelId { get; set; }
        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// Unix time (in seconds) of when the user started typing.
        /// </summary>
        [DataMember(Name = "timestamp")]
        public int Timestamp { get; set; }
    }
}
