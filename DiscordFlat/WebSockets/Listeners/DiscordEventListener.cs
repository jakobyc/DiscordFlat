using DiscordFlat.Serialization;
using DiscordFlat.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.Listeners
{
    public enum Events
    {
        GUILD_CREATE
    }

    /// <summary>
    /// Listen for events from a Discord WebSocket server.
    /// </summary>
    public class DiscordEventListener
    {
        private ArraySegment<byte> buffer;
        private JsonSerializer serializer;
        private DiscordWebSocket socket;

        public DiscordEventListener(DiscordWebSocket socket)
        {
            this.socket = socket;

            buffer = new ArraySegment<byte>(new byte[10000]);
            serializer = new JsonSerializer();
        }

        /// <summary>
        /// Listen for an event and then pass the response to the client.
        /// </summary>
        public async void Listen()
        {
            if (socket.Client.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await socket.Client.ReceiveAsync(buffer, CancellationToken.None);
                string response = Encoding.ASCII.GetString(buffer.Array).Replace("\0", "");

                string eventName = GetEventName(response);
                string opCode = GetOpCode(response);

                // Asynchronous callback:
                Task t = new Task(() => Callback(response, eventName, opCode));
                t.Start();

                // Recursively listen for events while the socket state is open:
                Listen();
            }
        }

        /// <summary>
        /// Retrieve results from the WebSocket server.
        /// </summary>
        /// <returns></returns>
        public async Task<T> ReceiveAsync<T>()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1000]);

            return await ReceiveAsync<T>(buffer);
        }

        // TODO: Abstract this to a DiscordEventListener class:
        public async Task<T> ReceiveAsync<T>(ArraySegment<byte> buffer)
        {
            WebSocketReceiveResult results = await socket.Client.ReceiveAsync(buffer, CancellationToken.None);
            string result = Encoding.ASCII.GetString(buffer.Array).Replace("\0", "");
            T response = serializer.Deserialize<T>(result);

            return response;
        }

        private void Callback(string response, string eventName, string opCode)
        {
            if (!string.IsNullOrEmpty(eventName))
            {
                switch(eventName)
                {
                    case Globals.Events.GuildCreate:
                        socket.OnGuildCreate(response);
                        break;
                }
            }
        }

        private string GetEventName(string response)
        {
            int index = response.IndexOf("\"t\":\"") + ("\"t\":\"").Length;
            int eventIndex = response.IndexOf('"', index + 1);

            string eventName = response.Substring(index, eventIndex - index);

            if (eventName.Contains("null"))
            {
                eventName = null;
            }

            return eventName;
        }

        private string GetOpCode(string response)
        {
            int index = response.IndexOf("\"op\":") + ("\"op\":").Length;
            int eventIndex = response.IndexOf(',', index + 1);

            string opCode = response.Substring(index, eventIndex - index);

            if (opCode.Contains("null"))
            {
                opCode = null;
            }

            return opCode;
        }
    }
}
