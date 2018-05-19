using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Channels
{
    [DataContract]
    public class Attachment
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "filename")]
        public string FileName { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "proxy_url")]
        public string ProxyUrl { get; set; }

        [DataMember(Name = "size")]
        public int Size { get; set; }
        [DataMember(Name = "height")]
        public int Height { get; set; }
        [DataMember(Name = "width")]
        public int Width { get; set; }
    }
}
