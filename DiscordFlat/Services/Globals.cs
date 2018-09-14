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
                                GuildMemberAdd = "GUILD_MEMBER_ADD",
                                GuildMemberRemove = "GUILD_MEMBER_REMOVE",
                                GuildMemberUpdate = "GUILD_MEMBER_UPDATE",
                                MessageCreate = "MESSAGE_CREATE",
                                MessageDelete = "MESSAGE_DELETE",
                                MessageUpdate = "MESSAGE_UPDATE",
                                PresenceUpdate = "PRESENCE_UPDATE",
                                Ready = "READY",
                                RequestGuildMembers = "REQUEST_GUILD_MEMBERS",
                                Resumed = "RESUMED",
                                TypingStart = "TYPING_START",
                                UserUpdate = "USER_UPDATE";
        }
    }
}
