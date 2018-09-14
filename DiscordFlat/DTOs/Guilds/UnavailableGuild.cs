using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Guilds
{
    [DataContract]
    public class UnavailableGuild
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "unavailable")]
        public bool Unavailable { get; set; }
    }
}
