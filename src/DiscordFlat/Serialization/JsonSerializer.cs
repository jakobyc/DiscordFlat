using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.Serialization
{
    public class JsonSerializer
    {
        private DataContractJsonSerializer serializer;

        /// <summary>
        /// Return a serialized object as a JSON string.
        /// </summary>
        public string Serialize(object o)
        {
            serializer = new DataContractJsonSerializer(o.GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, o);

                return Encoding.ASCII.GetString(stream.ToArray()) ?? "";
            }
        }

        /// <summary>
        /// Return a deserialized object.
        /// </summary>
        public TType Deserialize<TType>(string json)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TType));

            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(json)))
            {
                return (TType)serializer.ReadObject(stream);
            }
        }
    }
}
