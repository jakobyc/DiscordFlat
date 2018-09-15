using DiscordFlat.DTOs.Authorization;
using DiscordFlat.WebSockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiscordFlat.Tests
{
    [TestClass]
    public class WebSocketTests
    {
        private DiscordWebSocketClient client = new DiscordWebSocketClient();

        public WebSocketTests()
        {
            TokenResponse token = new TokenResponse(true)
            {
                AccessToken = AppConfig.GetApiKey()
            };
            
            if (token != null && !string.IsNullOrEmpty(token.AccessToken))
            {
                bool connected = client.Connect(token.AccessToken).Result;
            }
            else
            {
                throw new System.Exception("Invalid token.");
            }
        }

        [TestMethod]
        public void IsConnected()
        {
            Assert.IsNotNull(client);
            Assert.IsTrue(client.IsConnected());
        }
    }
}
