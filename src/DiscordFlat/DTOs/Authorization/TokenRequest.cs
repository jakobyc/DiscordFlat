using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DiscordFlat.DTOs.Authorization
{
    [DataContract]
    public class TokenRequest
    {
        [DataMember(Name="client_id")]
        public string ClientId { get; set; }
        [DataMember(Name = "client_secret")]
        public string ClientSecret { get; set; }
        [DataMember(Name = "grant_type")]
        public string GrantType { get; set; }
        [DataMember(Name = "code")]
        public string Code { get; set; }
        [DataMember(Name = "redirect_uri")]
        public string RedirectUri { get; set; }
    }
}