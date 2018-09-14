using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Guilds;

namespace DiscordFlat.Managers
{
    public interface IDiscordGuildManager
    {
        bool AddUserToRole(string guildId, string userId, string roleId);
        bool AddUserToRole(TokenResponse tokenResponse, string guildId, string userId, string roleId);
        bool RemoveRoleFromUser(string guildId, string userId, string roleId);
        bool RemoveRoleFromUser(TokenResponse tokenResponse, string guildId, string userId, string roleId);

        GuildMembers GetMembers(string guildId, int limit);
        GuildMembers GetMembers(TokenResponse tokenResponse, string guildId, int limit);

        GuildRoles GetRoles(string guildId);
        GuildRoles GetRoles(TokenResponse tokenResponse, string guildId);

        GuildRole GetRole(string roleName);
        GuildRole GetRole(GuildRoles roles, string roleName);
    }
}