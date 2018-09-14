using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Users;

namespace DiscordFlat.Managers
{
    public interface IDiscordUserManager
    {
        DiscordUser GetCurrentUser();
        DiscordUser GetCurrentUser(TokenResponse tokenResponse);
    }
}