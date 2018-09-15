using DiscordFlat.DTOs.Authorization;
using DiscordFlat.WebSockets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

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
                AccessToken = AppConfig.Config["apiKey"]
            };
            
            if (token != null && !string.IsNullOrEmpty(token.AccessToken))
            {
                client.Connect(token.AccessToken).GetAwaiter();
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
