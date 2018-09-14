using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public class UpdateStatus
    {
        [DataMember(Name = "since")]
        public int? Since { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "afk")]
        public bool Afk { get; set; }

        [DataMember(Name = "game")]
        public Game Game { get; set; }
    }
}
