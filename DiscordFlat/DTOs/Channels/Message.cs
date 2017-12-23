using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace DiscordFlat.DTOs.Channels
{
    [DataContract]
    public class Message
    {
        [DataMember(Name="id")]
        public string Id { get; set; }
        [DataMember(Name = "channel_id")]
        public string ChannelId { get; set; }
        // TODO: Create Author object
        public string Author { get; set; }
        [DataMember(Name = "content")]
        public string Content { get; set; }
        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }
        [DataMember(Name = "edited_timestamp")]
        public string EditedTimestamp { get; set; }
        [DataMember(Name = "nonce")]
        public string Nonce { get; set; }
        [DataMember(Name = "webhook_id")]
        public string WebhookId { get; set; }

        [DataMember(Name = "tts")]
        public bool Tts { get; set; }
        [DataMember(Name = "mention_everyone")]
        public bool MentionEveryone { get; set; }
        [DataMember(Name = "pinned")]
        public bool Pinned { get; set; }

        [DataMember(Name = "type")]
        public int Type { get; set; }
    }
}
