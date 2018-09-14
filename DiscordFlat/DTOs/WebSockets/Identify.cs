using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public class Identify
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "properties")]
        public IdentifyConnection Properties { get; set; }

        [DataMember(Name = "compress")]
        public bool Compress { get; set; }

        [DataMember(Name = "large_threshold")]
        public int LargeThreshold { get; set; }

        [DataMember(Name = "shard")]
        public int[] Shards { get; set; }

        /*[DataMember(Name = "presence")]
        public UpdateStatus Presence { get; set; }*/
    }
}
