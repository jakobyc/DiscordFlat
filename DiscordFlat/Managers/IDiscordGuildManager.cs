﻿using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Guilds;

namespace DiscordFlat.Managers
{
    public interface IDiscordGuildManager
    {
        bool AddUserToRole(TokenResponse tokenResponse, string guildId, string userId, string roleId);
        bool RemoveRoleFromUser(TokenResponse tokenResponse, string guildId, string userId, string roleId);

        GuildMembers GetMembers(TokenResponse tokenResponse, string guildId, int limit);

        GuildRoles GetRoles(TokenResponse tokenResponse, string guildId);
        GuildRole GetRole(GuildRoles roles, string roleName);

    }
}