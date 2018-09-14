using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlatCore.DTOs.Channels
{
    public class Messages : List<Message>, IRetrievable, ICollection<Message>
    {
        public string PathUrl { get => "channels/{channel}/messages"; }
    }
}
