using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Guilds;

namespace DiscordFlat.Managers
{
    public interface IDiscordGuildManager
    {
        bool AddUserToRole(TokenResponse tokenResponse, string guildId, string userId, string roleId);
        GuildRoles GetRoles(TokenResponse tokenResponse, string guildId);
    }
}