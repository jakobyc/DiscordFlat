using DiscordFlat.DTOs.Channels;
using DiscordFlat.DTOs.WebSockets.Events.Connections;
using DiscordFlat.DTOs.WebSockets.Events.Guilds;
using DiscordFlat.DTOs.WebSockets.Events.Messages;
using DiscordFlat.DTOs.WebSockets.Heartbeats;
using DiscordFlat.Serialization;
using DiscordFlat.WebSockets.EventHandlers.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.WebSockets.EventHandlers
{
    public class DiscordEventHandler
    {
        public event EventHandler<DiscordOnGuildCreateEventArgs> OnGuildCreate;
        public event EventHandler<DiscordOnHeartbeatEventArgs> OnHeartbeat;
        public event EventHandler<DiscordOnMessageEventArgs> OnMessage;
        public event EventHandler<DiscordOnReadyEventArgs> OnReady;
        public event EventHandler<DiscordOnResumeEventArgs> OnResume;

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

        #region Event: Heartbeat
        /// <summary>
        /// Invoked when a GUILD_CREATE event is fired from Discord's WebSocket server.
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

    }
}
