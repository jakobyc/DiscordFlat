using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Channels;

namespace DiscordFlat.Managers
{
    public interface IDiscordChannelManager
    {
        bool CreateMessage(string webhookId, string webhookToken, string message);

        Messages GetMessages(TokenResponse tokenResponse, string channelId);
        Message GetMessage(TokenResponse tokenResponse, string channelId, string messageId);
    }
}