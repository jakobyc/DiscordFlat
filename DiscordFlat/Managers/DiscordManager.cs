using DiscordFlatCore.DTOs.Authorization;
using DiscordFlatCore.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DiscordFlatCore.Managers
{
    public static class ContentType
    {
        public const string Json = "application/json",
                            MultipartForm = "multipart/form-data";
    }
    public abstract class DiscordManager
    {
        protected JsonSerializer Serializer;

        public DiscordManager()
        {
            Serializer = new JsonSerializer();
        }

        public WebClient GetWebClient(TokenResponse token)
        {
            WebClient client = new WebClient();
            client.Headers.Add(HttpRequestHeader.Authorization, token.Type + " " + token.AccessToken);

            return client;
        }

        public WebClient GetWebClient(TokenResponse token, string type)
        {
            WebClient client = GetWebClient(token);
            client.Headers.Add(HttpRequestHeader.ContentType, type);

            return client;
        }
    }
}
