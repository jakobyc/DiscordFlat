using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DiscordFlatCore.DTOs.Users
{
    [DataContract]
    public class DiscordUser : IRetrievable
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "discriminator")]
        public string Discriminator { get; set; }
        [DataMember(Name = "avatar")]
        public string Avatar { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "bot")]
        public bool Bot { get; set; }
        [DataMember(Name = "mfa_enabled")]
        public bool MfaEnabled { get; set; }
        [DataMember(Name = "verified")]
        public bool Verified { get; set; }

        public string PathUrl { get => "users/@me"; }
    }
}