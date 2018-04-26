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
        /// Post a message in a channel. A bot must have connected to a gateway at least once to use this.
        /// </summary>
        bool CreateMessage(string channelId, string message);
        /// <summary>
        /// Post a message in a channel. A bot must have connected to a gateway at least once to use this.
        /// </summary>
        bool CreateMessage(TokenResponse tokenResponse, string channelId, string message);

        /// <summary>
        /// Delete a message from a channel.
        /// </summary>
        bool DeleteMessage(string channelId, string messageId);
        /// <summary>
        /// Delete a message from a channel.
        /// </summary>
        bool DeleteMessage(TokenResponse tokenResponse, string channelId, string messageId);

        /// <summary>
        /// Get all messages from a channel.
        /// </summary>
        Messages GetMessages(string channelId);
        /// <summary>
        /// Get all messages from a channel.
        /// </summary>
        Messages GetMessages(TokenResponse tokenResponse, string channelId);

        /// <summary>
        /// Get a specific message from a channel.
        /// </summary>
        Message GetMessage(string channelId, string messageId);
        /// <summary>
        /// Get a specific message from a channel.
        /// </summary>
        Message GetMessage(TokenResponse tokenResponse, string channelId, string messageId);
    }
}