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
    }
}
