using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.Channels
{
    [DataContract]
    public class Embed
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }

        [DataMember(Name = "color")]
        public int Color { get; set; }

    }
}
