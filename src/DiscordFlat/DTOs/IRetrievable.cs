using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.DTOs
{
    public interface IRetrievable
    {
        /// <summary>
        /// Path Url for retrieving an object via API.
        /// </summary>
        string PathUrl { get; }
    }
}
