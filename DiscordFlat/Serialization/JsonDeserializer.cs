using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.Serialization
{
    public class JsonDeserializer
    {
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
