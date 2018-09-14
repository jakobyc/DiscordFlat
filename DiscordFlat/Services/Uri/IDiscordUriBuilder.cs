using DiscordFlatCore.DTOs;
using System.Collections.Generic;

namespace DiscordFlatCore.Services.Uri
{
    public interface IDiscordUriBuilder
    {
        IDiscordUriBuilder AddEndOfPath();
        IDiscordUriBuilder AddPath(IRetrievable obj);
        IDiscordUriBuilder AddPath(string path);
        IDiscordUriBuilder AddParameter(string parameter, string value, bool operand);
        IDiscordUriBuilder AddParameters(IDictionary<string, string> parameters);
        string Build();
    }
}