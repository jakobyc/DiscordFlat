using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.WebSockets;
using DiscordFlat.DTOs.WebSockets.Events.Connections;
using DiscordFlat.Serialization;
using DiscordFlat.Services.Uri;
using DiscordFlat.WebSockets.EventHandlers.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets
{
    public class DiscordWebSocketClient
    {
        public Uri Uri { get => new Uri("wss://gateway.discord.gg?v=6&encoding=json"); }

        private DiscordWebSocket socket;
        private int shardId = 0,
                    shardCount = 1;

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
        public async Task<bool> Connect(string token)
        {
            shardCount = GetGatewayBot(token).Shards;
            bool connected = await socket.ConnectAndIdentify(token, shardId, shardCount);

            if (connected)
            {
                shardId++;
            }

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

        /// <summary>
        /// Identify a Gateway Bot.
        /// </summary>
        public async Task<bool> Identify(string token)
        {
            shardCount = GetGatewayBot(token).Shards;
            ReadyResponse ready = await socket.Identify(token, shardId, shardCount);

            if (ready != null)
            {
                shardId++;
                return true;
            }

            return false;
        }
        
        public void Disconnect()
        {
            socket.Disconnect();
            shardId--;
        }

        public bool IsConnected()
        {
            if (socket.GetSocketState() == WebSocketState.Open)
            {
                return true;
            }
            return false;
        }

        public GatewayBot GetGatewayBot(string token)
        {
            GatewayBot bot = new GatewayBot();

            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.Authorization, TokenType.Bot + " " + token);
                try
                {
                    IDiscordUriBuilder uriBuilder = new DiscordUriBuilder();
                    string uri = uriBuilder.AddPath(bot)
                                           .Build();

                    string response = client.DownloadString(uri);

                    JsonSerializer serializer = new JsonSerializer();
                    bot = serializer.Deserialize<GatewayBot>(response);
                }
                catch (Exception) { }
            }

            return bot;
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
        /// Add a callback that will occur when the WebSocket server requests a heartbeat from our client. Supports multiple callbacks.
        /// </summary>
        public void OnHeartbeatRequest(EventHandler<DiscordOnHeartbeatRequestEventArgs> e)
        {
            socket.Handler.OnHeartbeatRequest += e;
        }

        /// <summary>
        /// Add a callback that will occur when the MESSAGE_CREATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnMessage(EventHandler<DiscordOnMessageEventArgs> e)
        {
            socket.Handler.OnMessage += e;
        }

        /// <summary>
        /// Add a callback that will occur when the PRESENCE_UPDATE event fires. Supports multiple callbacks.
        /// </summary>
        public void OnPresenceUpdate(EventHandler<DiscordOnPresenceUpdateEventArgs> e)
        {
            socket.Handler.OnPresenceUpdate += e;
        }

        /// <summary>
        /// Add a callback that will occur when the READY event fires. Supports multiple callbacks.
        /// </summary>
        public void OnReady(EventHandler<DiscordOnReadyEventArgs> e)
        {
            socket.Handler.OnReady += e;
        }

        /// <summary>
        /// Add a callback that will occur when the RESUMED event fires. Supports multiple callbacks.
        /// </summary>
        public void OnResume(EventHandler<DiscordOnResumeEventArgs> e)
        {
            socket.Handler.OnResume += e;
        }

        /// <summary>
        /// Add a callback that will occur when the socket's state changes. Supports multiple callbacks.
        /// </summary>
        public void OnStateChange(EventHandler<DiscordOnStateChangeEventArgs> e)
        {
            socket.Handler.OnStateChange += e;
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
