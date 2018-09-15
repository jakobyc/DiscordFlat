using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordFlat.Tests
{
    public class AppConfig
    {
        private static IConfiguration instance { get; } = Init();
        
        public static string GetApiKey()
        {
            return instance?["apiKey"];
        }

        private static IConfiguration Init()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.test.json")
                                             .Build();
        }
    }
}
