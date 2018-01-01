using System.Threading.Tasks;
using DiscordFlat.WebSockets;

namespace DiscordFlat.Bots
{
    public interface IDiscordGatewayBot
    {
        void AddCommand(string command, string message);
        Task<bool> Connect(DiscordWebSocketClient client, int shardId, int shardCount);
        void ListenForCommands();
    }
}