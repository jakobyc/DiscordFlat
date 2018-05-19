using DiscordFlatCore.DTOs.Authorization;
using DiscordFlatCore.Managers;
using DiscordFlatCore.WebSockets.EventHandlers.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.Bots.Commands
{
    public class BotCommands
    {
        private IDictionary<string, string> commands;
        private IDiscordChannelManager channelManager;
        private TokenResponse token;

        public BotCommands(TokenResponse token) : this (token, new ChannelManager(token))
        {
            commands = new Dictionary<string, string>();

            this.token = token;
        }

        public BotCommands(TokenResponse token, IDiscordChannelManager channelManager)
        {
            commands = new Dictionary<string, string>();
            this.channelManager = channelManager;

            this.token = token;
        }

        public void AddCommand(string command, string message)
        {
            commands.Add(command, message);
        }

        public void ReadMessage(object sender, DiscordOnMessageEventArgs e)
        {
            if (e.Message != null)
            {
                if (!string.IsNullOrEmpty(e.Message.Content))
                {
                    foreach (KeyValuePair<string, string> command in commands)
                    {
                        if (command.Key == e.Message.Content)
                        {
                            channelManager.CreateMessage(token, e.Message.ChannelId, command.Value);
                        }
                    }
                }
            }
        }
    }
}
