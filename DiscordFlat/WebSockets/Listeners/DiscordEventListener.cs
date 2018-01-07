using DiscordFlat.DTOs.WebSockets;
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
    /// <summary>
    /// Listen for events from a Discord WebSocket server.
    /// </summary>
    public class DiscordEventListener
    {
        public bool Listening { get; private set; }

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
        public async Task Listen()
        {
            try
            {
                if (socket.Client.State == WebSocketState.Open)
                {
                    Listening = true;
                    WebSocketReceiveResult result = null;
                    do
                    {
                        result = await socket.Client.ReceiveAsync(buffer, CancellationToken.None);
                    }
                    while (!result.EndOfMessage);
                    Listening = false;

                    if (result.Count > 0)
                    {
                        string response = Encoding.ASCII.GetString(buffer.Array).Replace("\0", "");

                        if (!string.IsNullOrEmpty(response))
                        {
                            string eventName = GetEventName(response);
                            string opCode = GetOpCode(response);

                            // Asynchronous callback:
                            Task t = new Task(() => Callback(response, eventName, opCode));
                            t.Start();
                        }
                    }

                    // Recursively listen for events while the socket state is open:
                    buffer = new ArraySegment<byte>(new byte[10000]);
                    await Listen();
                }
                else
                {
                    await socket.Resume();
                    await Listen();
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Retrieve results from the WebSocket server.
        /// </summary>
        /// <returns></returns>
        public async Task<T> ReceiveAsync<T>()
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[5000]);

            return await ReceiveAsync<T>(buffer);
        }

        public async Task<T> ReceiveAsync<T>(ArraySegment<byte> buffer)
        {
            WebSocketReceiveResult results = await socket.Client.ReceiveAsync(buffer, CancellationToken.None);
            string result = Encoding.ASCII.GetString(buffer.Array).Replace("\0", "");
            T response = serializer.Deserialize<T>(result);

            return response;
        }

        private void Callback(string response, string eventName, string opCode)
        {
            if (!string.IsNullOrEmpty(response))
            {
                if (!string.IsNullOrEmpty(eventName))
                {
                    switch (eventName)
                    {
                        case Globals.Events.GuildCreate:
                            socket.Handler.GuildCreated(response);
                            break;
                        case Globals.Events.GuildMemberAdd:
                            socket.Handler.GuildMemberAdded(response);
                            break;
                        case Globals.Events.GuildMemberRemove:
                            socket.Handler.GuildMemberRemoved(response);
                            break;
                        case Globals.Events.GuildMemberUpdate:
                            socket.Handler.GuildMemberUpdated(response);
                            break;
                        case Globals.Events.MessageCreate:
                            socket.Handler.MessageReceived(response);
                            break;
                        case Globals.Events.PresenceUpdate:
                            socket.Handler.PresenceUpdated(response);
                            break;
                        case Globals.Events.Resumed:
                            socket.Handler.Resumed(response);
                            break;
                        case Globals.Events.TypingStart:
                            socket.Handler.TypingStarted(response);
                            break;
                    }
                }

                int op;
                bool parsed = int.TryParse(opCode, out op);
                if (!string.IsNullOrEmpty(opCode) && parsed)
                {
                    switch (op)
                    {
                        case ((int)OpCodes.HeartbeatAcknowledged):
                            socket.Handler.HeartbeatAcknowledged(response);
                            break;
                    }
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
