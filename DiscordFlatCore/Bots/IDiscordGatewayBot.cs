using System.Threading.Tasks;
using DiscordFlatCore.Bots.Roles;
using DiscordFlatCore.WebSockets;

namespace DiscordFlatCore.Bots
{
    public interface IDiscordGatewayBot
    {
        BotGuildManager Guilds { get; set; }

        void AddCommand(string command, string message);
        Task<bool> Connect(DiscordWebSocketClient client);
        void ListenForCommands();
    }
}