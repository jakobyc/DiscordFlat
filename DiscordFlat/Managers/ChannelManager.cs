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
    public class ChannelManager : IDiscordChannelManager
    {
        private JsonDeserializer deserializer;

        public ChannelManager()
        {
            this.deserializer = new JsonDeserializer();
        }

        public Messages GetMessages(TokenResponse tokenResponse, string channelId)
        {
            Messages messages = new Messages();

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(messages.PathUrl.Replace("{channel}", channelId))
                                           .Build();
                    string response = client.DownloadString(uri);

                    messages = deserializer.Deserialize<Messages>(response);
                }
                catch (Exception) { }
            }
            return messages;
        }

        public Message GetMessage(TokenResponse tokenResponse, string channelId, string messageId)
        {
            Message message = new Message();

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(message.PathUrl.Replace("{channel}", channelId).Replace("{message}", messageId))
                                           .Build();
                    string response = client.DownloadString(uri);

                    message = deserializer.Deserialize<Message>(response);
                }
                catch (Exception) { }
            }
            return message;
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
                    string uri = uriBuilder.AddPath(string.Format("webhooks/{0}/{1}", webhookId, webhookToken))
                                           .Build();

                    string json = string.Format("{{ \"content\":\"{0}\" }}", message);
                    string response = client.UploadString(uri, "POST", json);

                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        public bool CreateMessage(TokenResponse tokenResponse, string channelId, string message)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(string.Format("channels/{0}/messages", channelId))
                                           .Build();

                    string json = string.Format("{{ \"content\":\"{0}\" }}", message);
                    string response = client.UploadString(uri, "POST", json);

                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }

        public bool DeleteMessage(TokenResponse tokenResponse, string channelId, string messageId)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(string.Format("channels/{0}/messages/{1}", channelId, messageId))
                                           .Build();

                    //string json = string.Format("{{ \"content\":\"{0}\" }}", message);
                    string response = client.UploadString(uri, "DELETE", "");

                    return true;
                }
                catch (Exception) { }
            }
            return false;
        }
    }
}
