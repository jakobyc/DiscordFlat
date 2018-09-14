using DiscordFlat.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordFlat.Services.Uri
{
    public class DiscordUriBuilder : IDiscordUriBuilder
    {
        private string Uri;

        public DiscordUriBuilder()
        {
            Uri = Globals.Uri.Base;
        }

        public IDiscordUriBuilder AddEndOfPath()
        {
            Uri += "?";

            return this;
        }

        public IDiscordUriBuilder AddPath(IRetrievable obj)
        {
            Uri += obj.PathUrl;

            return this;
        }

        public IDiscordUriBuilder AddPath(string path)
        {
            Uri += path;

            return this;
        }

        public IDiscordUriBuilder AddParameter(string parameter, string value, bool operand)
        {
            if (operand)
            {
                Uri += string.Format("{0}={1}&", parameter, value);
            }
            else
            {
                Uri += string.Format("{0}={1}", parameter, value);
            }

            return this;
        }

        public IDiscordUriBuilder AddParameters(IDictionary<string, string> parameters)
        {
            for (int i = 0; i < parameters.Count; i++)
            {
                if (i == parameters.Count - 1)
                {
                    Uri += string.Format("{0}={1}", parameters.ElementAt(i).Key, parameters.ElementAt(i).Value);
                }
                else
                {
                    Uri += string.Format("{0}={1}&", parameters.ElementAt(i).Key, parameters.ElementAt(i).Value);
                }
            }
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                Uri += string.Format("{0}={1}", pair.Key, pair.Value);
            }
            return this;
        }

        public string Build()
        {
            return Uri;
        }
    }
}
