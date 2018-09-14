using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DiscordFlat.DTOs.Channels
{
    [DataContract]
    public class ChannelConfig : IRetrievable
    {
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(Name = "topic", EmitDefaultValue = false)]
        public string Topic { get; set; }

        [DataMember(Name = "parent_id", EmitDefaultValue = false)]
        public string ParentId { get; set; }

        [DataMember(Name = "position")]
        public int Position { get; set; }
        [DataMember(Name = "bitrate", EmitDefaultValue = false)]
        public int Bitrate { get; set; }
        [DataMember(Name = "user_limit", EmitDefaultValue = false)]
        public int UserLimit { get; set; }

        [DataMember(Name = "nsfw", EmitDefaultValue = false)]
        public bool Nsfw { get; set; }

        // TODO: Add overwrite and overwrites DTO's
        /*[DataMember(Name = "permission_overwrites")]
        public object PermissionOverwrites { get; set; }*/

        public string PathUrl { get => "channels/{channel}"; }

        public ChannelConfig(Channel channel)
        {
            this.Name = channel.Name;
            this.Position = channel.Position;
            this.Topic = channel.Topic;
            this.Nsfw = channel.Nsfw;
            this.Bitrate = channel.Bitrate;
            this.UserLimit = channel.UserLimit;
            //this.PermissionOverwrites = channel.PermissionOverwrites;
            this.ParentId = channel.ParentId;
        }

        public ChannelConfig() { }

        public ChannelConfig(string name)
        {
            this.Name = name;
        }

        public ChannelConfig(string name, string topic) : this(name)
        {
            this.Topic = topic;
        }

        public ChannelConfig(string name, string topic, int position) : this(name, topic)
        {
            this.Position = position;
        }

        public bool IsValid()
        {
            if (Name == "")
            {
                throw new Exception("Name can not be empty.");
            }

            return true;
        }
    }
}
