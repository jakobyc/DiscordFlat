using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Guilds
{
    public class GuildMembers : List<GuildMember>, ICollection<GuildMember>, IRetrievable
    {
        public string PathUrl { get => "guilds/{guild}/members"; }
    }
}
