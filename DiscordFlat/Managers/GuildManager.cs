using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DiscordFlat.DTOs.Authorization;
using DiscordFlat.Services.Uri;
using DiscordFlat.DTOs.Guilds;
using DiscordFlat.Serialization;

namespace DiscordFlat.Managers
{
    public class GuildManager : IDiscordGuildManager
    {
        private JsonDeserializer deserializer;

        public GuildManager()
        {
            this.deserializer = new JsonDeserializer();
        }

        public bool AddUserToRole(TokenResponse tokenResponse, string guildId, string userId, string roleId)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    // TODO: May want a more elogant solution for building paths (no IRetrievable to get path from)
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath("guilds/")
                                           .AddPath(guildId + "/")
                                           .AddPath("members/")
                                           .AddPath(userId + "/")
                                           .AddPath("roles/")
                                           .AddPath(roleId)
                                           .Build();

                    string response = client.UploadString(uri, "PUT", "");
                    
                    if (response == "")
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception) { }

                return false;
            }
        }

        public GuildRoles GetRoles(TokenResponse tokenResponse, string guildId)
        {
            GuildRoles roles = new GuildRoles();

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(roles.PathUrl.Replace("{guild}", guildId))
                                           .Build();
                    string response = client.DownloadString(uri);

                    roles = deserializer.Deserialize<GuildRoles>(response);
                }
                catch(Exception e) { }
            }
            return roles;
        }

        public GuildRole GetRole(GuildRoles roles, string roleName)
        {
            GuildRole role = new GuildRole();
            role = roles.Where(x => x.Name == roleName).FirstOrDefault();

            return role;
        }
    }
}
