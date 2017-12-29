using DiscordFlat.DTOs.WebSockets;
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
            uri = new Uri("wss://gateway.discord.gg");
            cancelToken = new CancellationToken();
            serializer = new JsonSerializer();
        }

        public async Task<HelloObject> Connect()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1000]);

            await client.ConnectAsync(uri, cancelToken);

            // Receive the "Hello" payload:
            HelloObject response = await ReceiveAsync<HelloObject>();
            string json = serializer.Serialize(response);
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
                string heartbeat = string.Format("{{\"op\": 1,\"d\": {0}}}", sequenceNumber);
                await SendAsync(heartbeat);

                HelloObject heartbeatResponse = await ReceiveAsync<HelloObject>();

                timer.Reset();
            }
        }

        public async Task Identify()
        {
            string payloadData = "{\"op\": 2,\"d\": null}";
            await SendAsync(payloadData);

            GatewayObject heartbeatResponse = await ReceiveAsync<GatewayObject>();
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
