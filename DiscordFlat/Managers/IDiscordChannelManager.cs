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
        /// Create a reaction to a message.
        /// </summary>
        bool CreateReaction(string channelId, string messageId, string emoji);
        /// <summary>
        /// Create a reaction to a message.
        /// </summary>
        bool CreateReaction(TokenResponse tokenResponse, string channelId, string messageId, string emoji);

        /// <summary>
        /// Delete a channel or close a private message.
        /// </summary>
        bool DeleteChannel(string channelId);
        /// <summary>
        /// Delete a channel or close a private message.
        /// </summary>
        bool DeleteChannel(TokenResponse tokenResponse, string channelId);

        /// <summary>
        /// Delete a message from a channel.
        /// </summary>
        bool DeleteMessage(string channelId, string messageId);
        /// <summary>
        /// Delete a message from a channel.
        /// </summary>
        bool DeleteMessage(TokenResponse tokenResponse, string channelId, string messageId);

        /// <summary>
        /// Get a channel.
        /// </summary>
        Channel GetChannel(string channelId);
        /// <summary>
        /// Get a channel.
        /// </summary>
        Channel GetChannel(TokenResponse tokenResponse, string channelId);

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

        /// <summary>
        /// Modify a channel's configurations.
        /// </summary>
        Channel ModifyChannel(string channelId, ChannelConfig config);
        /// <summary>
        /// Modify a channel's configurations.
        /// </summary>
        Channel ModifyChannel(TokenResponse tokenResponse, string channelId, ChannelConfig config);
    }
}