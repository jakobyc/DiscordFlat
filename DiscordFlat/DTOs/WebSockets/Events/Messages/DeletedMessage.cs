using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets.Events.Messages
{
    [DataContract]
    public class DeletedMessage
    {
        [DataMember(Name = "id")]
        public string MessageId { get; set; }
        [DataMember(Name = "channel_id")]
        public string ChannelId { get; set; }
    }
}
