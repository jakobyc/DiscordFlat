using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.Services
{
    public static class Globals
    {
        public static class Uri
        {
            public const string Base = "https://discordapp.com/api/";
        }

        public static class Events
        {
            public const string GuildCreate = "GUILD_CREATE",
                                MessageCreate = "MESSAGE_CREATE",
                                Ready = "READY";
        }
    }
}
