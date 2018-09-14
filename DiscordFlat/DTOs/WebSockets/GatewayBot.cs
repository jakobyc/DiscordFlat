using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public class GatewayBot : IRetrievable
    {
        /// <summary>
        /// WebSocket endpoint.
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "shards")]
        public int Shards { get; set; }

        public string PathUrl { get => "gateway/bot"; }
    }
}
