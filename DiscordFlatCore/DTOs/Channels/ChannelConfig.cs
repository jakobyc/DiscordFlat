using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DiscordFlatCore.DTOs.Channels
{
    [DataContract]
    public class ChannelConfig : IRetrievable
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /*[DataMember(Name = "topic")]
        public string Topic { get; set; }

        [DataMember(Name = "parent_id")]
        public string ParentId { get; set; }

        [DataMember(Name = "position")]
        public int Position { get; set; }
        [DataMember(Name = "bitrate")]
        public int Bitrate { get; set; }
        [DataMember(Name = "user_limit")]
        public int UserLimit { get; set; }

        [DataMember(Name = "nsfw")]
        public bool Nsfw { get; set; }*/

        // TODO: Add overwrite and overwrites DTO's
        /*[DataMember(Name = "permission_overwrites")]
        public object PermissionOverwrites { get; set; }*/

        public string PathUrl { get => "channels/{channel}"; }

        public ChannelConfig(Channel channel)
        {
            this.Name = channel.Name;
            /*this.Position = channel.Position;
            this.Topic = channel.Topic;
            this.Nsfw = channel.Nsfw;
            this.Bitrate = channel.Bitrate;
            this.UserLimit = channel.UserLimit;
            this.PermissionOverwrites = channel.PermissionOverwrites;
            this.ParentId = channel.ParentId;*/
        }
    }
}
