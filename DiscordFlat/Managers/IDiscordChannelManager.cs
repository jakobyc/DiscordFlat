using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Channels;

namespace DiscordFlat.Managers
{
    public interface IDiscordChannelManager
    {
        /// <summary>
        /// Post a message through a Webhook.
        /// </summary>
        bool CreateMessage(string webhookId, string webhookToken, string message);
        /// <summary>
        /// A bot must have connected to a gateway at least once to use this.
        /// </summary>
        bool CreateMessage(TokenResponse tokenResponse, string channelId, string message);
        bool DeleteMessage(TokenResponse tokenResponse, string channelId, string messageId);

        Messages GetMessages(TokenResponse tokenResponse, string channelId);
        Message GetMessage(TokenResponse tokenResponse, string channelId, string messageId);
    }
}