using DiscordFlatCore.DTOs.Channels;
using DiscordFlatCore.DTOs.WebSockets.Events.Connections;
using DiscordFlatCore.DTOs.WebSockets.Events.Guilds;
using DiscordFlatCore.DTOs.WebSockets.Events.Messages;
using DiscordFlatCore.DTOs.WebSockets.Events.Users;
using DiscordFlatCore.DTOs.WebSockets.Heartbeats;
using DiscordFlatCore.Serialization;
using DiscordFlatCore.WebSockets.EventHandlers.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.WebSockets.EventHandlers
{
    public class DiscordEventHandler
    {
        public event EventHandler<DiscordOnGuildCreateEventArgs> OnGuildCreate;
        public event EventHandler<DiscordOnGuildMemberAddEventArgs> OnGuildMemberAdd;
        public event EventHandler<DiscordOnGuildMemberRemoveEventArgs> OnGuildMemberRemove;
        public event EventHandler<DiscordOnGuildMemberUpdateEventArgs> OnGuildMemberUpdate;
        public event EventHandler<DiscordOnHeartbeatEventArgs> OnHeartbeat;
        public event EventHandler<DiscordOnHeartbeatRequestEventArgs> OnHeartbeatRequest;
        public event EventHandler<DiscordOnMessageEventArgs> OnMessage;
        public event EventHandler<DiscordOnPresenceUpdateEventArgs> OnPresenceUpdate;
        public event EventHandler<DiscordOnReadyEventArgs> OnReady;
        public event EventHandler<DiscordOnResumeEventArgs> OnResume;
        public event EventHandler<DiscordOnStateChangeEventArgs> OnStateChange;
        public event EventHandler<DiscordOnTypingStartEventArgs> OnTypingStart;

        private JsonSerializer serializer;

        public DiscordEventHandler()
        {
            serializer = new JsonSerializer();
        }

        #region Event: GUILD_CREATE
        /// <summary>
        /// Invoked when a GUILD_CREATE event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void GuildCreated(string response)
        {
            GuildCreate guild = serializer.Deserialize<GuildCreate>(response);
            DiscordOnGuildCreateEventArgs args = new DiscordOnGuildCreateEventArgs()
            {
                Guild = guild.EventData
            };

            OnGuildCreateNotify(args);
        }

        protected void OnGuildCreateNotify(DiscordOnGuildCreateEventArgs e)
        {
            EventHandler<DiscordOnGuildCreateEventArgs> handler = OnGuildCreate;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: GUILD_MEMBER_ADD
        /// <summary>
        /// Invoked when a GUILD_MEMBER_ADD event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void GuildMemberAdded(string response)
        {
            GuildMemberAdd guild = serializer.Deserialize<GuildMemberAdd>(response);
            DiscordOnGuildMemberAddEventArgs args = new DiscordOnGuildMemberAddEventArgs()
            {
                GuildMember = guild.EventData
            };

            OnGuildMemberAddNotify(args);
        }

        protected void OnGuildMemberAddNotify(DiscordOnGuildMemberAddEventArgs e)
        {
            EventHandler<DiscordOnGuildMemberAddEventArgs> handler = OnGuildMemberAdd;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: GUILD_MEMBER_REMOVE
        /// <summary>
        /// Invoked when a GUILD_MEMBER_REMOVE event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void GuildMemberRemoved(string response)
        {
            GuildMemberRemove guild = serializer.Deserialize<GuildMemberRemove>(response);
            DiscordOnGuildMemberRemoveEventArgs args = new DiscordOnGuildMemberRemoveEventArgs()
            {
                GuildMember = guild.EventData
            };

            OnGuildMemberRemoveNotify(args);
        }

        protected void OnGuildMemberRemoveNotify(DiscordOnGuildMemberRemoveEventArgs e)
        {
            EventHandler<DiscordOnGuildMemberRemoveEventArgs> handler = OnGuildMemberRemove;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: GUILD_MEMBER_UPDATE
        /// <summary>
        /// Invoked when a GUILD_MEMBER_UPDATE event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void GuildMemberUpdated(string response)
        {
            GuildMemberUpdate guild = serializer.Deserialize<GuildMemberUpdate>(response);
            DiscordOnGuildMemberUpdateEventArgs args = new DiscordOnGuildMemberUpdateEventArgs()
            {
                GuildMember = guild.EventData
            };

            OnGuildMemberUpdateNotify(args);
        }

        protected void OnGuildMemberUpdateNotify(DiscordOnGuildMemberUpdateEventArgs e)
        {
            EventHandler<DiscordOnGuildMemberUpdateEventArgs> handler = OnGuildMemberUpdate;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: Heartbeat ACK
        /// <summary>
        /// Invoked when a Heartbeat Acknowledged event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void HeartbeatAcknowledged(string response)
        {
            HeartbeatResponse heartbeat = serializer.Deserialize<HeartbeatResponse>(response);
            DiscordOnHeartbeatEventArgs args = new DiscordOnHeartbeatEventArgs()
            {
                Heartbeat = heartbeat
            };

            OnHeartbeatNotify(args);
        }

        protected void OnHeartbeatNotify(DiscordOnHeartbeatEventArgs e)
        {
            EventHandler<DiscordOnHeartbeatEventArgs> handler = OnHeartbeat;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: Heartbeat Request
        /// <summary>
        /// Invoked when a Discord's WebSocket server requests a heartbeat from our client.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void HeartbeatRequested(string response)
        {
            Heartbeat heartbeat = serializer.Deserialize<Heartbeat>(response);
            DiscordOnHeartbeatRequestEventArgs args = new DiscordOnHeartbeatRequestEventArgs()
            {
                Heartbeat = heartbeat
            };

            OnHeartbeatRequestNotify(args);
        }

        protected void OnHeartbeatRequestNotify(DiscordOnHeartbeatRequestEventArgs e)
        {
            EventHandler<DiscordOnHeartbeatRequestEventArgs> handler = OnHeartbeatRequest;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: MESSAGE_CREATE
        /// <summary>
        /// Invoked when a MESSAGE_CREATE event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void MessageReceived(string response)
        {
            MessageCreate message = serializer.Deserialize<MessageCreate>(response);
            DiscordOnMessageEventArgs args = new DiscordOnMessageEventArgs()
            {
                Message = message.EventData
            };

            OnMessageNotify(args);
        }

        protected void OnMessageNotify(DiscordOnMessageEventArgs e)
        {
            EventHandler<DiscordOnMessageEventArgs> handler = OnMessage;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: PRESENCE_UPDATE
        /// <summary>
        /// Invoked when a PRESENCE_UPDATE event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void PresenceUpdated(string response)
        {
            PresenceUpdate presence = serializer.Deserialize<PresenceUpdate>(response);
            DiscordOnPresenceUpdateEventArgs args = new DiscordOnPresenceUpdateEventArgs()
            {
                Presence = presence.EventData
            };

            OnPresenceUpdateNotify(args);
        }

        protected void OnPresenceUpdateNotify(DiscordOnPresenceUpdateEventArgs e)
        {
            EventHandler<DiscordOnPresenceUpdateEventArgs> handler = OnPresenceUpdate;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: READY
        /// <summary>
        /// Invoked when a READY event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void IsReady(string response)
        {
            ReadyResponse ready = serializer.Deserialize<ReadyResponse>(response);
            DiscordOnReadyEventArgs args = new DiscordOnReadyEventArgs()
            {
                Ready = ready.EventData
            };

            OnReadyNotify(args);
        }

        protected void OnReadyNotify(DiscordOnReadyEventArgs e)
        {
            EventHandler<DiscordOnReadyEventArgs> handler = OnReady;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: RESUMED
        /// <summary>
        /// Invoked when a RESUMED event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void Resumed(string response)
        {
            Resumed resume = serializer.Deserialize<Resumed>(response);
            DiscordOnResumeEventArgs args = new DiscordOnResumeEventArgs()
            {
                Trace = resume.Trace
            };

            OnResumeNotify(args);
        }

        protected void OnResumeNotify(DiscordOnResumeEventArgs e)
        {
            EventHandler<DiscordOnResumeEventArgs> handler = OnResume;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: Socket State Change
        /// <summary>
        /// Invoked when a RESUMED event is fired from Discord's WebSocket server.
        /// </summary>
        /// <param name="response">JSON response.</param>
        public void StateChanged(WebSocketState state)
        {
            DiscordOnStateChangeEventArgs args = new DiscordOnStateChangeEventArgs()
            {
                State = state
            };

            OnStateChangeNotify(args);
        }

        protected void OnStateChangeNotify(DiscordOnStateChangeEventArgs e)
        {
            EventHandler<DiscordOnStateChangeEventArgs> handler = OnStateChange;
            handler?.Invoke(this, e);
        }
        #endregion

        #region Event: TYPING_START
        public void TypingStarted(string response)
        {
            TypingStartResponse typingStart = serializer.Deserialize<TypingStartResponse>(response);
            DiscordOnTypingStartEventArgs args = new DiscordOnTypingStartEventArgs()
            {
                TypingStart = typingStart.EventData
            };

            OnTypingStartNotify(args);
        }

        protected void OnTypingStartNotify(DiscordOnTypingStartEventArgs e)
        {
            EventHandler<DiscordOnTypingStartEventArgs> handler = OnTypingStart;
            handler?.Invoke(this, e);
        }
        #endregion
    }
}
