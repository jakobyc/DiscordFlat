using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.Guilds
{
    public class GuildRoles : List<GuildRole>, ICollection<GuildRole>, IRetrievable
    {
        public string PathUrl { get => "guilds/{guild}/roles"; }
    }
}
