using DiscordFlatCore.DTOs.WebSockets;
using DiscordFlatCore.DTOs.WebSockets.Events.Connections;
using DiscordFlatCore.Serialization;
using DiscordFlatCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordFlatCore.WebSockets.Listeners
{
    /// <summary>
    /// Listen for events from a Discord WebSocket server.
    /// </summary>
    public class DiscordEventListener
    {
        public bool Listening { get; private set; }
        public CancellationTokenSource CancelToken { get; private set; }

        private ArraySegment<byte> buffer;
        private JsonSerializer serializer;
        private DiscordWebSocket socket;

        public DiscordEventListener(DiscordWebSocket socket)
        {
            this.socket = socket;

            buffer = new ArraySegment<byte>(new byte[10000]);
            serializer = new JsonSerializer();
            CancelToken = new CancellationTokenSource();
        }

        /// <summary>
        /// Listen for an event and then pass the response to the client.
        /// </summary>
        public async Task Listen()
        {
            if (!CancelToken.IsCancellationRequested)
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
                                CacheSequence(response);

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
                        StateChange(socket.GetSocketState());
                        await socket.Resume();
                        await Listen();
                    }
                }
                catch (Exception)
                {
                    StateChange(socket.GetSocketState());
                    await socket.Resume();
                    await Listen();
                }
            }
            else
            {
                Console.WriteLine("Listener disconnected.");

                // Cleanup cancellation token:
                CancelToken.Dispose();
                CancelToken = new CancellationTokenSource();
            }
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

            // If the response is a ReadyResponse, invoke callback(s):
            if (response is ReadyResponse)
            {
                Callback(result, Globals.Events.Ready, ((int)OpCodes.Identify).ToString());
            }

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
                        case Globals.Events.Ready:
                            socket.Handler.IsReady(response);
                            // Update socket's cache:
                            socket.Cache.ReadyResponse = serializer.Deserialize<ReadyResponse>(response);
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
                        // Heartbeat request from WebSocket server:
                        case ((int)OpCodes.Heartbeat):
                            socket.Handler.HeartbeatRequested(response);
                            break;
                        case ((int)OpCodes.HeartbeatAcknowledged):
                            socket.Handler.HeartbeatAcknowledged(response);
                            break;
                        case ((int)OpCodes.Reconnect):
                            #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                            socket.Resume();
                            #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                            break;
                        case ((int)OpCodes.InvalidSession):
                            InvalidSession session = serializer.Deserialize<InvalidSession>(response);
                            if (session.Resumable)
                            {
                                #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                                socket.Resume();
                                #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                            }
                            else
                            {
                                #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                                // TODO: Remove hard-coded shard info:
                                socket.Identify("", 0, 1);
                                #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

                            }
                            break;
                    }
                }
            }
        }

        private void StateChange(WebSocketState state)
        {
            socket.Handler.StateChanged(state);
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

        /// <summary>
        /// Cache the sequence number received from Discord's WebSocket server.
        /// </summary>
        private void CacheSequence(string response)
        {
            int index = response.IndexOf("\"s\":") + ("\"s\":").Length;
            int eventIndex = response.IndexOf(',', index + 1);

            string eventName = response.Substring(index, eventIndex - index);

            if (!eventName.Contains("null"))
            {
                int sequence;
                bool parsed = int.TryParse(eventName, out sequence);

                if (parsed)
                {
                    socket.Cache.ReadyResponse.SequenceNumber = sequence;
                }
            }

        }
    }
}
