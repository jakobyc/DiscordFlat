using DiscordFlat.DTOs.WebSockets;
using DiscordFlat.DTOs.WebSockets.Events.Connections;
using DiscordFlat.DTOs.WebSockets.Events.Guilds;
using DiscordFlat.DTOs.WebSockets.Events.Messages;
using DiscordFlat.DTOs.WebSockets.Heartbeats;
using DiscordFlat.Managers;
using DiscordFlat.Serialization;
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

        private DiscordEventListener listener;
        private Uri uri;
        private CancellationToken cancelToken;
        private JsonSerializer serializer;

        public DiscordWebSocket()
        {
            listener = new DiscordEventListener(this);
            uri = new Uri("wss://gateway.discord.gg?v=6&encoding=json");
            cancelToken = new CancellationToken();
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
            await Client.ConnectAsync(uri, cancelToken);

            bool receivedHello = await Hello();
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

        protected async void Heartbeat(HelloObject gatewayObject)
        {
            Stopwatch timer = new Stopwatch();

            while (Client.State == WebSocketState.Open)
            {
                timer.Start();

                // Wait to send the heartbeat based on the heartbeat interval:
                while (timer.ElapsedMilliseconds < gatewayObject.EventData.HeartbeatInterval - 1000)
                {

                }

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

                    await SendAsync(message);

                    timer.Reset();
                }
            }
        }

        public async Task Resume(string token, string sessionId, int? sequenceNumber)
        {
            GatewayResumeObject resumeObj = new GatewayResumeObject();
            resumeObj.Resume = new GatewayResume();
            resumeObj.Resume.Token = token;
            resumeObj.Resume.SessionId = sessionId;
            resumeObj.Resume.SequenceNumber = sequenceNumber;

            string message = serializer.Serialize(resumeObj);

            // Send Gateway Resume payload:
            await SendAsync(message);
            // Replay missed events and finish with a Resumed event
            listener.Listen();
        }

        public async Task<ReadyResponse> Identify(string token, int shardId, int shardCount)
        {
            ReadyResponse ready = new ReadyResponse();

            if (Client.State == WebSocketState.Open)
            {
                IdentifyObject identify = new IdentifyObject();
                identify.OpCode = 2;

                identify.EventData = new Identify();
                identify.EventData.Compress = false;
                identify.EventData.LargeThreshold = 50;
                identify.EventData.Token = token;
                identify.EventData.Shards = new int[] { shardId, shardCount };

                identify.EventData.Properties = new IdentifyConnection();
                identify.EventData.Properties.OperatingSystem = "Windows";
                identify.EventData.Properties.Browser = "Library";
                identify.EventData.Properties.Device = "Library";

                string payload = serializer.Serialize(identify);
                await SendAsync(payload);

                ready = await listener.ReceiveAsync<ReadyResponse>();
                listener.Listen();
                return ready;
            }

            // Identification failed, return null.
            return null;
        }

        /// <summary>
        /// Asynchronously list for events.
        /// </summary>
        public void ListenForEvents()
        {
            Task t = new Task(() => listener.Listen());
            t.Start();
        }

        #region Events:
        /// <summary>
        /// Called when event GUILD_CREATE is fired.
        /// </summary>
        public GuildCreate OnGuildCreate(string response)
        {
            GuildCreate guild = serializer.Deserialize<GuildCreate>(response);

            return guild;
        }

        /// <summary>
        /// Called when event CREATE_MESSAGE is fired.
        /// </summary>
        public MessageCreate OnMessage(string response)
        {
            MessageCreate message = serializer.Deserialize<MessageCreate>(response);

            return message;
        }

        public Resumed OnResume(string response)
        {
            Resumed resumed = serializer.Deserialize<Resumed>(response);

            return resumed;
        }

        public HeartbeatResponse OnHeartbeat(string response)
        {
            HeartbeatResponse beat = serializer.Deserialize<HeartbeatResponse>(response);

            if (beat.OpCode == (int)OpCodes.HeartbeatAcknowledged)
            {

            }
            return beat;
        }
        #endregion

        public void Disconnect()
        {
            try
            {
                Client.Abort();
            }
            catch (Exception) { }
        }

        public async Task RequestGuildMembers()
        {
            RequestGuildMembers request = new RequestGuildMembers();
            request.EventData = new GuildMembersRequest();
            request.OpCode = (int)OpCodes.RequestGuildMembers;
            request.SequenceNumber = 1;
            request.EventName = "REQUEST_GUILD_MEMBERS";
            request.EventData.GuildId = "393599551728123904";
            request.EventData.Limit = 0;
            request.EventData.Query = "";

            string payload = serializer.Serialize(request);

            await SendAsync(payload);
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
