using DiscordFlatCore.DTOs.Authorization;
using DiscordFlatCore.DTOs.Users;

namespace DiscordFlatCore.Managers
{
    public interface IDiscordUserManager
    {
        DiscordUser GetCurrentUser();
        DiscordUser GetCurrentUser(TokenResponse tokenResponse);
    }
}