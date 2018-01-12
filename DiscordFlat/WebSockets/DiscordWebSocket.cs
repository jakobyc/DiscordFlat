using DiscordFlat.DTOs.Authorization;
using DiscordFlat.DTOs.WebSockets;
using DiscordFlat.DTOs.WebSockets.Events.Connections;
using DiscordFlat.DTOs.WebSockets.Events.Guilds;
using DiscordFlat.DTOs.WebSockets.Events.Messages;
using DiscordFlat.DTOs.WebSockets.Heartbeats;
using DiscordFlat.Managers;
using DiscordFlat.Serialization;
using DiscordFlat.Services;
using DiscordFlat.WebSockets.Caches;
using DiscordFlat.WebSockets.EventHandlers;
using DiscordFlat.WebSockets.Listeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets
{
    public class DiscordWebSocket
    {
        public ClientWebSocket Client;
        public DiscordWebSocketCache Cache;
        public DiscordEventHandler Handler;

        private DiscordEventListener listener;
        private Uri uri;
        private CancellationToken cancelToken;
        private JsonSerializer serializer;
        private bool listening = false;

        public DiscordWebSocket() : this (new Uri("wss://gateway.discord.gg?v=6&encoding=json"), new CancellationToken())
        {
        }

        public DiscordWebSocket(Uri uri, CancellationToken cancelToken)
        {
            this.uri = uri;
            this.cancelToken = cancelToken;

            listener = new DiscordEventListener(this);
            Handler = new DiscordEventHandler();
            Cache = new DiscordWebSocketCache();
            serializer = new JsonSerializer();
        }

        /// <summary>
        /// Attempt to connect to a WebSocket server and identify your bot user.
        /// </summary>
        /// <param name="token">Bot access token.</param>
        /// <param name="shardId">Bot shard id.</param>
        /// <returns>Connection status.</returns>
        public async Task<bool> ConnectAndIdentify(string token, int shardId, int shardCount)
        {
            bool receivedHello = await Connect();
            if (receivedHello)
            {
                ReadyResponse response = await Identify(token, shardId, shardCount);
                if (response != null)
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Attempt to connect to a WebSocket.
        /// </summary>
        /// <returns>Connection and hello payload status.</returns>
        public async Task<bool> Connect()
        {
            Client = new ClientWebSocket();
            await Client.ConnectAsync(uri, cancelToken);

            return await Hello();
        }

        /// <summary>
        /// Heartbeat loop for the WebSocket connection.
        /// </summary>
        protected async void Heartbeat(HelloObject gatewayObject)
        {
            CancellationTokenSource heartbeatWait = new CancellationTokenSource();

            while (Client.State == WebSocketState.Open)
            {
                // Wait to send the heartbeat based on the heartbeat interval:
                heartbeatWait.Token.WaitHandle.WaitOne(gatewayObject.EventData.HeartbeatInterval - 1000);

                // Check state again for thread safety:
                if (Client.State == WebSocketState.Open)
                {
                    string sequenceNumber = gatewayObject.SequenceNumber.ToString();
                    if (string.IsNullOrEmpty(sequenceNumber))
                    {
                        sequenceNumber = "null";
                    }
                    
                    // Send heartbeat:
                    Heartbeat heartbeat = new Heartbeat();
                    heartbeat.EventData = sequenceNumber;

                    string message = serializer.Serialize(heartbeat);

                    try
                    {
                        await SendAsync(message);
                    }
                    catch(Exception) { }
                }
            }
        }

        /// <summary>
        /// If an Identify request has previously been sent successfully, use this to resume off a cached session Id.
        /// </summary>
        /// <returns></returns>
        public async Task Resume()
        {
            if (Client.State != WebSocketState.Open)
            {
                Client = new ClientWebSocket();
                await Connect();
                //await Client.ConnectAsync(uri, cancelToken);
            }
            GatewayResumeObject resumeObj = new GatewayResumeObject();
            resumeObj.Resume = new GatewayResume();
            resumeObj.Resume.Token = Cache.Token.AccessToken;
            resumeObj.Resume.SessionId = Cache.ReadyResponse.EventData.SessionId;
            resumeObj.Resume.SequenceNumber = Cache.ReadyResponse.SequenceNumber;

            string message = serializer.Serialize(resumeObj);

            // Send Gateway Resume payload:
            await SendAsync(message);

            // Replay missed events and finish with a Resumed event
        }

        /// <summary>
        /// Attempt to send an Identify request to the server WebSocket. If previously identified, a Resume request will be sent instead.
        /// </summary>
        public async Task<ReadyResponse> Identify(string token, int shardId, int shardCount)
        {
            ReadyResponse ready = new ReadyResponse();

            // Cache the token:
            if (!string.IsNullOrEmpty(token))
            {
                Cache.Token = new TokenResponse();
                Cache.Token.AccessToken = token; 
            }

            if (Client.State == WebSocketState.Open)
            {
                IdentifyObject identify = new IdentifyObject();
                identify.OpCode = 2;

                identify.EventData = new Identify();
                identify.EventData.Compress = false;
                identify.EventData.LargeThreshold = 50;
                identify.EventData.Token = Cache.Token.AccessToken;
                identify.EventData.Shards = new int[] { shardId, shardCount };

                identify.EventData.Properties = new IdentifyConnection();
                identify.EventData.Properties.OperatingSystem = "Windows";
                identify.EventData.Properties.Browser = "Library";
                identify.EventData.Properties.Device = "Library";

                string payload = serializer.Serialize(identify);
                await SendAsync(payload);

                // Listen on a new thread:
                if (!listening)
                {
                    ready = await listener.ReceiveAsync<ReadyResponse>();
                    if (ready != null)
                    {
                        Cache.ReadyResponse = ready;
                    }

                    Task t = new Task(async () => await listener.Listen());
                    listening = true;

                    t.Start();
                }
                return ready;
            }

            // Identification failed, return null.
            return null;
        }

        public void Disconnect()
        {
            try
            {
                Client.Abort();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Send a request for guild members.
        /// </summary>
        /// <param name="query">String that username starts with, or an empty string to return all members.</param>
        public async Task RequestGuildMembers(string guildId, string query)
        {
            RequestGuildMembers request = new RequestGuildMembers();
            request.EventData = new GuildMembersRequest();
            request.OpCode = (int)OpCodes.RequestGuildMembers;
            request.SequenceNumber = 1;
            request.EventName = Globals.Events.RequestGuildMembers;
            request.EventData.GuildId = guildId;
            request.EventData.Limit = 0;
            request.EventData.Query = query;

            string payload = serializer.Serialize(request);

            await SendAsync(payload);
        }

        public WebSocketState GetSocketState()
        {
            if (Client != null)
            {
                return Client.State;
            }
            return WebSocketState.Closed;
        }

        private async Task SendAsync(string payload)
        {
            ArraySegment<byte> message = new ArraySegment<byte>(Encoding.ASCII.GetBytes(payload));
            await Client.SendAsync(message, WebSocketMessageType.Binary, true, CancellationToken.None);
        }

        private async Task<bool> Hello()
        {
            // Receive the "Hello" payload:
            HelloObject response = await listener.ReceiveAsync<HelloObject>();

            // Spin up thread for heartbeat:
            if (response.OpCode == (int)OpCodes.Hello)
            {
                Task t = new Task(() => Heartbeat(response));
                t.Start();

                return true;
            }
            else
            {
                await Client.CloseAsync(WebSocketCloseStatus.Empty, "Did not receive a proper 'Hello' response from Discord's server.", CancellationToken.None);
            }

            return false;
        }
    }
}
