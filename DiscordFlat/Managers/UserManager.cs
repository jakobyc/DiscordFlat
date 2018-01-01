using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.Users;
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
    public class UserManager : IDiscordUserManager
    {
        private JsonDeserializer deserializer;

        public UserManager()
        {
            this.deserializer = new JsonDeserializer();
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
