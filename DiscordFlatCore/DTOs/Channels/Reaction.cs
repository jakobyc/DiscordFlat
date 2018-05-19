using DiscordFlatCore.DTOs.Emojis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Channels
{
    [DataContract]
    public class Reaction
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "me")]
        public bool Me { get; set; }

        [DataMember(Name = "emoji")]
        public Emoji Emoji { get; set; }
    }
}
