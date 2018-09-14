using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Guilds;
using DiscordFlat.DTOs.Users;
using DiscordFlat.Managers;
using DiscordFlat.WebSockets.EventHandlers.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.Bots.Roles
{
    public class BotGuildManager : GuildManager
    {
        private TokenResponse token;

        public BotGuildManager(TokenResponse token)
        {
            this.token = token;
        }

        #region Events
        // TODO: Is this friendly for use with public bots?
        /// <summary>
        /// Add a role to users when they join a guild.
        /// </summary>
        public void AddRoleOnJoin(WebSockets.DiscordWebSocketClient client, string roleId)
        {
            client.OnGuildMemberAdd((sender, e) => AddRoleOnJoin(sender, e, roleId));
        }

        private void AddRoleOnJoin(object sender, DiscordOnGuildMemberAddEventArgs e, string roleId)
        {
            if (!string.IsNullOrEmpty(e.GuildMember.GuildId) && !string.IsNullOrEmpty(e.GuildMember.User.Id))
            {
                AddUserToRole(e.GuildMember.GuildId, e.GuildMember.User.Id, roleId);
            }
        }
        #endregion

    }
}
