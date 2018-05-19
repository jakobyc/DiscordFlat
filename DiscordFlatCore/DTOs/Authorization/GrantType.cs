using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Authorization
{
    public static class GrantType
    {
        public const string AuthorizationCode = "authorization_code",
                            ClientCredentials = "client_credentials",
                            RefreshToken = "refresh_token";
    }
}
