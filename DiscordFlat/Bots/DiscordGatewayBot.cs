using DiscordFlat.Bots.Commands;
using DiscordFlat.Bots.Roles;
using DiscordFlat.DTOs.Authorization;
using DiscordFlat.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.Bots
{
    public class DiscordGatewayBot : IDiscordGatewayBot
    {
        public BotGuildManager Guilds { get; set; }

        private DiscordWebSocketClient client;
        private BotCommands commands;
        private TokenResponse token;

        public DiscordGatewayBot(string token)
        {
            this.token = new TokenResponse(true)
            {
                AccessToken = token
            };

            commands = new BotCommands(this.token);
            Guilds = new BotGuildManager(this.token);
        }

        public DiscordGatewayBot(TokenResponse token)
        {
            this.token = token;
            if (token.Type != TokenType.Bot)
            {
                token.Type = TokenType.Bot;
            }

            commands = new BotCommands(this.token);
            Guilds = new BotGuildManager(this.token);
        }

        public async Task<bool> Connect(DiscordWebSocketClient client)
        {
            this.client = client;

            return await client.Connect(token.AccessToken);
        }

        #region Commands
        /// <summary>
        /// Add a user command that the bot will respond to (i.e., users typing a !hi command and the bot responding with "Hello").
        /// </summary>
        /// <param name="command">User command (i.e., !hi)</param>
        /// <param name="message">Bot's response to a command.</param>
        public void AddCommand(string command, string message)
        {
            commands.AddCommand(command, message);
        }

        public void ListenForCommands()
        {
            client.OnMessage(commands.ReadMessage);
        }
        #endregion

        public void AddRoleOnJoin(string roleId)
        {
            Guilds.AddRoleOnJoin(client, roleId);
        }
    }
}
