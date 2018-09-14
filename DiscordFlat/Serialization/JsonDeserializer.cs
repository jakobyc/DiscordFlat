using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.Serialization
{
    // Use JsonSerializer instead
    public class JsonDeserializer
    {
        public T Deserialize<T>(string json)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes(json)))
            {
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}
