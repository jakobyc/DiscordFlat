using DiscordFlat.DTOs.WebSockets;
using DiscordFlat.DTOs.WebSockets.Events;
using DiscordFlat.DTOs.WebSockets.Heartbeats;
using DiscordFlat.Serialization;
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
        private ClientWebSocket client;
        private Uri uri;
        private CancellationToken cancelToken;
        private JsonSerializer serializer;

        public DiscordWebSocket()
        {
            client = new ClientWebSocket();
            uri = new Uri("wss://gateway.discord.gg?v=6&encoding=json");
            cancelToken = new CancellationToken();
            serializer = new JsonSerializer();
        }

        public async Task<HelloObject> Connect()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1000]);

            await client.ConnectAsync(uri, cancelToken);

            // Receive the "Hello" payload:
            HelloObject response = await ReceiveAsync<HelloObject>();
            
            // Spin up thread for heartbeat:
            Task t = new Task(() => Heartbeat(response));
            t.Start();

            return response;
        }

        public async void Heartbeat(HelloObject gatewayObject)
        {
            Stopwatch timer = new Stopwatch();

            while (client.State == WebSocketState.Open)
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

                HeartbeatResponse heartbeatResponse = await ReceiveAsync<HeartbeatResponse>();

                timer.Reset();
            }
        }

        public async Task Identify(string token, int shardId)
        {
            IdentifyObject identify = new IdentifyObject();
            identify.OpCode = 2;

            identify.EventData = new Identify();
            identify.EventData.Compress = false;
            identify.EventData.LargeThreshold = 50;
            identify.EventData.Token = token;
            identify.EventData.Shards = new int[] { shardId, 10 };

            identify.EventData.Properties = new IdentifyConnection();
            identify.EventData.Properties.OperatingSystem = "Windows";
            identify.EventData.Properties.Browser = "Google";
            identify.EventData.Properties.Device = "Google";

            string payload = serializer.Serialize(identify);
            await SendAsync(payload);
            
            ReadyResponse heartbeatResponse = await ReceiveAsync<ReadyResponse>();
        }

        /// <summary>
        /// Retrieve results from the WebSocket server.
        /// </summary>
        /// <returns></returns>
        private async Task<T> ReceiveAsync<T>()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1000]);

            WebSocketReceiveResult results = await client.ReceiveAsync(buffer, cancelToken);

            string result = Encoding.ASCII.GetString(buffer.Array).Replace("\0", "");
            T heartbeatResponse = serializer.Deserialize<T>(result);

            return heartbeatResponse;
        }

        private async Task SendAsync(string payloadData)
        {
            ArraySegment<byte> message = new ArraySegment<byte>(Encoding.ASCII.GetBytes(payloadData));
            await client.SendAsync(message, WebSocketMessageType.Text, true, cancelToken);
        }
    }
}
