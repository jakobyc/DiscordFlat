using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Users;
using DiscordFlat.DTOs.Channels;
using DiscordFlat.Serialization;
using DiscordFlat.Services.Uri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat
{
    public class DiscordClient
    {
        private JsonDeserializer deserializer;

        public DiscordClient()
        {
            this.deserializer = new JsonDeserializer();
        }

        /// <summary>
        /// Get OAuth2 access token.
        /// </summary>
        /// <param name="code">Code received from Discord in exchange for the client Id.</param>
        /// <param name="redirectUri">Redirect Uri configured on a Discord application.</param>
        public TokenResponse GetAccessToken(string clientId, string clientSecret, string code, string redirectUri)
        {
            TokenResponse tokenResponse = new TokenResponse();

            //const string tokenUrl = "https://discordapp.com/api/oauth2/token";

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(tokenResponse)
                                           .AddEndOfPath()
                                           .AddParameter("client_id", clientId, true)
                                           .AddParameter("client_secret", clientSecret, true)
                                           .AddParameter("grant_type", GrantType.AuthorizationCode, true)
                                           .AddParameter("code", code, true)
                                           .AddParameter("redirect_uri", redirectUri, false)
                                           .Build();
                    /*string uri = string.Format("{0}?client_id={1}&client_secret={2}&grant_type=authorization_code&code={3}&redirect_uri={4}", tokenUrl,
                                                                                                                                              clientId,
                                                                                                                                              clientSecret,
                                                                                                                                              code,
                                                                                                                                              redirectUri);*/
                    string response = client.UploadString(uri, "POST", "");

                    tokenResponse = deserializer.Deserialize<TokenResponse>(response);
                }
                catch (Exception) { }
            }
            return tokenResponse;
        }

        public DiscordUser GetUser(TokenResponse tokenResponse)
        {
            DiscordUser user = new DiscordUser();
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    //https://discordapp.com/api/users/@me
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(user)
                                           .Build();
                    string response = client.DownloadString(uri);

                    user = deserializer.Deserialize<DiscordUser>(response);
                }
                catch (Exception) { }
            }
            return user;
        }

        public Messages GetMessages(TokenResponse tokenResponse, string channelId)
        {
            Messages messages = new Messages();

            using (WebClient client = new WebClient())
            {
                //https://discordapp.com/api/channels/393599551728123906/messages
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
            //return user;
        }
    }
}
