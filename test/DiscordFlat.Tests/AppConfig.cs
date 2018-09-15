using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordFlat.Tests
{
    public class AppConfig
    {
        public static IConfiguration Config { get; } = Init();

        private static IConfiguration Init()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.test.json")
                                             .Build();
        }
    }
}
