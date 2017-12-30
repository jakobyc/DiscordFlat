using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.WebSockets
{
    [DataContract]
    public class GuildMembersRequest
    {
        [DataMember(Name = "guild_id")]
        public string GuildId { get; set; }
        /// <summary>
        /// String that username starts with, or an empty string to return all members.
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>
        /// Maximum number of members to receive, or 0 to receive all matched members.
        /// </summary>
        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
