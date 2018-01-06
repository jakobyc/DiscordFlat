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
using DiscordFlat.DTOs.Users;

namespace DiscordFlat.Managers
{
    public class GuildManager : IDiscordGuildManager
    {
        private JsonSerializer serializer;

        public GuildManager()
        {
            this.serializer = new JsonSerializer();
        }

        public bool AddUserToRole(TokenResponse tokenResponse, string guildId, string userId, string roleId)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(string.Format("guilds/{0}/members/{1}/roles/{2}", guildId, userId, roleId))
                                           .Build();

                    string response = client.UploadString(uri, "PUT", "");
                    
                    if (response == "")
                    {
                        return true;
                    }
                }
                catch (Exception) { }

                return false;
            }
        }

        /// <summary>
        /// Return a list of Guild Members.
        /// </summary>
        /// <param name="limit">Max number of members to return (1-1000)</param>
        /// <returns></returns>
        public GuildMembers GetMembers(TokenResponse tokenResponse, string guildId, int limit)
        {
            GuildMembers members = new GuildMembers();
            string memberLimit = limit.ToString();

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(members.PathUrl.Replace("{guild}", guildId))
                                           .AddEndOfPath()
                                           .AddParameter("limit", memberLimit, false)
                                           .Build();
                    string response = client.DownloadString(uri);

                    members = serializer.Deserialize<GuildMembers>(response);
                }
                catch (Exception) { }
            }
            return members;
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

                    roles = serializer.Deserialize<GuildRoles>(response);
                }
                catch(Exception) { }
            }
            return roles;
        }

        public GuildRole GetRole(GuildRoles roles, string roleName)
        {
            GuildRole role = new GuildRole();

            roleName = roleName.ToUpper();
            role = roles.Where(x => x.Name.ToUpper() == roleName).FirstOrDefault();

            return role;
        }

        // TODO: Not functional:
        public bool ModifyUser(TokenResponse tokenResponse, ModifyGuildMember modification, string guildId, string userId)
        {
            throw new Exception("ModifyUser is not functional yet.");

            /*using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(string.Format("guilds/{0}/members/{1}", guildId, userId))
                                           .Build();

                    string json = serializer.Serialize(modification);

                    string response = client.UploadString(uri, "PATCH", json);

                    if (response == "")
                    {
                        return true;
                    }
                }
                catch (Exception) { }

                return false;
            }*/
        }

        public bool RemoveRoleFromUser(TokenResponse tokenResponse, string guildId, string userId, string roleId)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, tokenResponse.Type + " " + tokenResponse.AccessToken);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(string.Format("guilds/{0}/members/{1}/roles/{2}", guildId, userId, roleId))
                                           .Build();

                    string response = client.UploadString(uri, "DELETE", "");

                    if (response == "")
                    {
                        return true;
                    }
                }
                catch (Exception) { }

                return false;
            }
        }
    }
}
