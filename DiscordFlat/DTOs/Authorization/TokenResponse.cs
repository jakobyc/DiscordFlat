using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs.Authorization
{
    [DataContract]
    public class TokenResponse : IRetrievable
    {
        public TokenResponse() { }
        public TokenResponse(bool bot)
        {
            if (bot)
            {
                // TODO: Make global constant for this:
                this.Type = "Bot";
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
