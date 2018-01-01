using System.Threading.Tasks;
using DiscordFlat.Bots.Roles;
using DiscordFlat.WebSockets;

namespace DiscordFlat.Bots
{
    public interface IDiscordGatewayBot
    {
        BotGuildManager GuildManager { get; set; }

        void AddCommand(string command, string message);
        Task<bool> Connect(DiscordWebSocketClient client, int shardId, int shardCount);
        void ListenForCommands();
    }
}