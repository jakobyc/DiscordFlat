using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.Authorization
{
    public static class TokenType
    {
        public const string Bearer = "Bearer",
                            Bot = "Bot";
    }

    [DataContract]
    public class TokenResponse : IRetrievable
    {
        public TokenResponse() { }
        public TokenResponse(bool bot)
        {
            if (bot)
            {
                this.Type = TokenType.Bot;
            }
            else
            {
                this.Type = TokenType.Bearer;
            }
        }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "token_type")]
        public string Type { get; set; }
        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }
        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }
        [DataMember(Name = "scope")]
        public string Scope { get; set; }

        public string PathUrl { get => "oauth2/token"; }
    }
}
