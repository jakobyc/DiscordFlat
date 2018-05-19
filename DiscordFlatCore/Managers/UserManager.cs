using DiscordFlatCore.DTOs.Authorization;
using DiscordFlatCore.DTOs.Users;
using DiscordFlatCore.Serialization;
using DiscordFlatCore.Services.Uri;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.Managers
{
    public class UserManager : IDiscordUserManager
    {
        private JsonDeserializer deserializer;
        private TokenResponse token;

        public UserManager()
        {
            this.deserializer = new JsonDeserializer();
        }

        public UserManager(TokenResponse token) : this()
        {
            this.token = token;
        }

        public DiscordUser GetCurrentUser()
        {
            return GetCurrentUser(token);
        }

        public DiscordUser GetCurrentUser(TokenResponse tokenResponse)
        {
            DiscordUser user = new DiscordUser();
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
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
    }
}
