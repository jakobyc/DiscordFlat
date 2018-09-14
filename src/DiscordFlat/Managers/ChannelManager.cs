using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Channels;
using DiscordFlat.Serialization;
using DiscordFlat.Services.Uri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.Managers
{
    public class ChannelManager : DiscordManager, IDiscordChannelManager
    {
        private TokenResponse token;

        public ChannelManager() { }

        public ChannelManager(TokenResponse token)
        {
            this.token = token;
        }

        /// <summary>
        /// Post a message through a Webhook.
        /// </summary>
        public bool CreateMessage(string webhookId, string webhookToken, string message)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath($"webhooks/{webhookId}/{webhookToken}")
                                           .Build();

                    string json = $"{{ \"content\":\"{message}\" }}";
                    string response = client.UploadString(uri, "POST", json);

                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        /// <summary>
        /// Post a message in a channel.
        /// </summary>
        public bool CreateMessage(string channelId, string message)
        {
            return CreateMessage(token, channelId, message);
        }

        /// <summary>
        /// Post a message in a channel.
        /// </summary>
        public bool CreateMessage(TokenResponse tokenResponse, string channelId, string message)
        {
            using (WebClient client = GetWebClient(tokenResponse, ContentType.Json))
            {
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath($"channels/{channelId}/messages")
                                           .Build();

                    string json = $"{{ \"content\":\"{message}\" }}";
                    string response = client.UploadString(uri, "POST", json);

                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        /// <summary>
        /// Create a reaction for a message.
        /// </summary>
        public bool CreateReaction(string channelId, string messageId, string emoji)
        {
            return CreateReaction(token, channelId, messageId, emoji);
        }

        /// <summary>
        /// Create a reaction for a message.
        /// </summary>
        public bool CreateReaction(TokenResponse tokenResponse, string channelId, string messageId, string emoji)
        {
            using (WebClient client = GetWebClient(tokenResponse))
            {
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath($"channels/{channelId}/messages/{messageId}/reactions/{emoji}/@me")
                                           .Build();

                    string response = client.UploadString(uri, "PUT", "");

                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        /// <summary>
        /// Delete a channel.
        /// </summary>
        public bool DeleteChannel(string channelId)
        {
            return DeleteChannel(token, channelId);
        }

        /// <summary>
        /// Delete a channel.
        /// </summary>
        public bool DeleteChannel(TokenResponse tokenResponse, string channelId)
        {
            using (WebClient client = GetWebClient(tokenResponse))
            {
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath($"channels/{channelId}")
                                           .Build();

                    string response = client.UploadString(uri, "DELETE", "");

                    if (!string.IsNullOrEmpty(response))
                    {
                        return true;
                    }
                }
                catch (Exception) { }
            }
            return false;
        }

        /// <summary>
        /// Delete a message from a channel.
        /// </summary>
        public bool DeleteMessage(string channelId, string messageId)
        {
            return DeleteMessage(token, channelId, messageId);
        }

        /// <summary>
        /// Delete a message from a channel.
        /// </summary>
        public bool DeleteMessage(TokenResponse tokenResponse, string channelId, string messageId)
        {
            using (WebClient client = GetWebClient(tokenResponse))
            {
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath($"channels/{channelId}/messages/{messageId}")
                                           .Build();

                    string response = client.UploadString(uri, "DELETE", "");

                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        public Channel GetChannel(string channelId)
        {
            return GetChannel(token, channelId);
        }

        public Channel GetChannel(TokenResponse tokenResponse, string channelId)
        {
            Channel channel = new Channel();

            using (WebClient client = GetWebClient(tokenResponse))
            {
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(channel.PathUrl.Replace("{channel}", channelId))
                                           .Build();
                    string response = client.DownloadString(uri);

                    channel = Serializer.Deserialize<Channel>(response);
                }
                catch (Exception) { }
            }

            return channel;
        }

        /// <summary>
        /// Get all messages from a channel.
        /// </summary>
        public Messages GetMessages(string channelId)
        {
            return GetMessages(token, channelId);
        }

        /// <summary>
        /// Get all messages from a channel.
        /// </summary>
        public Messages GetMessages(TokenResponse tokenResponse, string channelId)
        {
            Messages messages = new Messages();

            using (WebClient client = GetWebClient(tokenResponse))
            {
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(messages.PathUrl.Replace("{channel}", channelId))
                                           .Build();
                    string response = client.DownloadString(uri);

                    messages = Serializer.Deserialize<Messages>(response);
                }
                catch (Exception) { }
            }
            return messages;
        }

        /// <summary>
        /// Get a specific message from a channel.
        /// </summary>
        public Message GetMessage(string channelId, string messageId)
        {
            return GetMessage(token, channelId, messageId);
        }

        /// <summary>
        /// Get a specific message from a channel.
        /// </summary>
        public Message GetMessage(TokenResponse tokenResponse, string channelId, string messageId)
        {
            Message message = new Message();

            using (WebClient client = GetWebClient(tokenResponse))
            {
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(message.PathUrl.Replace("{channel}", channelId).Replace("{message}", messageId))
                                           .Build();
                    string response = client.DownloadString(uri);

                    message = Serializer.Deserialize<Message>(response);
                }
                catch (Exception) { }
            }
            return message;
        }

        /// <summary>
        /// Modify a guild channel.
        /// </summary>
        /// <param name="config">Configuration file containing optional values to change.</param>
        public Channel ModifyChannel(string channelId, ChannelConfig config)
        {
            return ModifyChannel(token, channelId, config);
        }

        /// <summary>
        /// Modify a guild channel.
        /// </summary>
        /// <param name="config">Configuration file containing optional values to change.</param>
        public Channel ModifyChannel(TokenResponse tokenResponse, string channelId, ChannelConfig config)
        {
            Channel channel = new Channel();

            using (WebClient client = GetWebClient(tokenResponse, ContentType.Json))
            {
                if (config != null && config.IsValid())
                {
                    try
                    {

                        IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                        string uri = uriBuilder.AddPath(config.PathUrl.Replace("{channel}", channelId))
                                               .Build();

                        string json = Serializer.Serialize(config);
                        string response = client.UploadString(uri, "PATCH", json);

                        channel = Serializer.Deserialize<Channel>(response);
                    }
                    catch (Exception) { }
                }
            }

            return channel;
        }
    }
}
