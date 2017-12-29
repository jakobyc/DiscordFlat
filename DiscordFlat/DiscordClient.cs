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
using DiscordFlat.DTOs.WebSockets;

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

                    string response = client.UploadString(uri, "POST", "");

                    tokenResponse = deserializer.Deserialize<TokenResponse>(response);
                }
                catch (Exception) { }
            }
            return tokenResponse;
        }

        public GatewayBot GetGatewayBot(TokenResponse tokenResponse)
        {
            GatewayBot bot = new GatewayBot();

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(bot)
                                           .Build();

                    string response = client.DownloadString(uri);

                    bot = deserializer.Deserialize<GatewayBot>(response);
                }
                catch (Exception) { }
            }

            return bot;
        }
    }
}
