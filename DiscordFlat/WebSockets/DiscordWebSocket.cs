using DiscordFlat.DTOs.WebSockets;
using DiscordFlat.DTOs.WebSockets.Events;
using DiscordFlat.DTOs.WebSockets.Events.Guilds;
using DiscordFlat.DTOs.WebSockets.Heartbeats;
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
            Client = new ClientWebSocket();
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
        public async Task<bool> Connect(string token, int shardId)
        {
            await Client.ConnectAsync(uri, cancelToken);

            bool receivedHello = await Hello();
            if (receivedHello)
            {
                ReadyResponse response = await Identify(token, shardId);
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

        public async Task<ReadyResponse> Identify(string token, int shardId)
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
                identify.EventData.Shards = new int[] { shardId, 1 };

                identify.EventData.Properties = new IdentifyConnection();
                identify.EventData.Properties.OperatingSystem = "Windows";
                identify.EventData.Properties.Browser = "Library";
                identify.EventData.Properties.Device = "Library";

                string payload = serializer.Serialize(identify);
                await SendAsync(payload);

                ready = await listener.ReceiveAsync<ReadyResponse>();
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

        /// <summary>
        /// Called when event GUILD_CREATE is fired.
        /// </summary>
        /// <param name="response"></param>
        public void OnGuildCreate(string response)
        {
            GuildCreate guild = serializer.Deserialize<GuildCreate>(response);
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
            //await ReceiveAsync<object>();
        }

        public async Task<GuildCreate> GuildCreate()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[3000]);

            GuildCreate dispatch = await listener.ReceiveAsync<GuildCreate>(buffer);
            return dispatch;
        }

        private async Task SendAsync(string payloadData)
        {
            ArraySegment<byte> message = new ArraySegment<byte>(Encoding.ASCII.GetBytes(payloadData));
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
