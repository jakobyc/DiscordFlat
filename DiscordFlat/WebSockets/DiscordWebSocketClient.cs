using DiscordFlat.DTOs.WebSockets.Events.Connections;
using DiscordFlat.WebSockets.EventHandlers.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets
{
    public class DiscordWebSocketClient
    {
        private DiscordWebSocket socket;
        public Uri Uri { get => new Uri("wss://gateway.discord.gg?v=6&encoding=json"); }

        public DiscordWebSocketClient()
        {
            socket = new DiscordWebSocket(Uri, new CancellationToken());
        }

        public DiscordWebSocketClient(Uri uri)
        {
            socket = new DiscordWebSocket(uri, new CancellationToken());
        }

        /// <summary>
        /// Attempt to connect and identify your bot user with a WebSocket server.
        /// </summary>
        /// <returns>Status of connection and identification.</returns>
        public async Task<bool> Connect(string token, int shardId, int shardCount)
        {
            bool connected = await socket.ConnectAndIdentify(token, shardId, shardCount);

            return connected;
        }

        /// <summary>
        /// Attempt to establish a connection with a WebSocket server.
        /// </summary>
        /// <returns>Status of connection.</returns>
        public async Task<bool> Connect()
        {
            bool connected = await socket.Connect();

            return connected;
        }

        public async Task<bool> Identify(string token, int shardId, int shardCount)
        {
            ReadyResponse ready = await socket.Identify(token, shardId, shardCount);

            if (ready != null)
            {
                return true;
            }

            return false;
        }

        #region Events
        /// <summary>
        /// Add a callback that will occur when the GUILD_CREATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnGuildCreate(EventHandler<DiscordOnGuildCreateEventArgs> e)
        {
            socket.Handler.OnGuildCreate += e;
        }

        /// <summary>
        /// Add a callback that will occur when the GUILD_CREATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnGuildMemberAdd(EventHandler<DiscordOnGuildMemberAddEventArgs> e)
        {
            socket.Handler.OnGuildMemberAdd += e;
        }

        /// <summary>
        /// Add a callback that will occur when the GUILD_CREATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnGuildMemberRemove(EventHandler<DiscordOnGuildMemberRemoveEventArgs> e)
        {
            socket.Handler.OnGuildMemberRemove += e;
        }

        /// <summary>
        /// Add a callback that will occur when the GUILD_CREATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnGuildMemberUpdate(EventHandler<DiscordOnGuildMemberUpdateEventArgs> e)
        {
            socket.Handler.OnGuildMemberUpdate += e;
        }

        /// <summary>
        /// Add a callback that will occur when the GUILD_CREATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnHeartbeat(EventHandler<DiscordOnHeartbeatEventArgs> e)
        {
            socket.Handler.OnHeartbeat += e;
        }

        /// <summary>
        /// Add a callback that will occur when the MESSAGE_CREATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnMessage(EventHandler<DiscordOnMessageEventArgs> e)
        {
            socket.Handler.OnMessage += e;
        }

        public void OnMessageUnsubscribe(EventHandler<DiscordOnMessageEventArgs> e)
        {
            socket.Handler.OnMessage -= e;
        }

        /// <summary>
        /// Add a callback that will occur when the PRESENCE_UPDATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnPresenceUpdate(EventHandler<DiscordOnPresenceUpdateEventArgs> e)
        {
            socket.Handler.OnPresenceUpdate += e;
        }

        /// <summary>
        /// Add a callback that will occur when the RESUMED event fires. Supports multiple callbacks.
        /// </summary>
        public void OnResume(EventHandler<DiscordOnResumeEventArgs> e)
        {
            socket.Handler.OnResume += e;
        }

        /// <summary>
        /// Add a callback that will occur when the TYPING_START event fires. Supports multiple callbacks.
        /// </summary>
        public void OnTypingStart(EventHandler<DiscordOnTypingStartEventArgs> e)
        {
            socket.Handler.OnTypingStart += e;
        }
        #endregion
    }
}
