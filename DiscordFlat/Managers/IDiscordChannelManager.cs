using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Channels;

namespace DiscordFlat.Managers
{
    public interface IDiscordChannelManager
    {
        Messages GetMessages(TokenResponse tokenResponse, string channelId);
        Message GetMessage(TokenResponse tokenResponse, string channelId, string messageId);
    }
}