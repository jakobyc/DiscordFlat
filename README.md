# DiscordFlat
.NET wrapper for Discord's API (https://discordapp.com/developers/docs/intro)

## How to use:
```
// WebSocket with a Gateway Bot:
DiscordWebSocketClient client = new DiscordWebSocketClient();
DiscordGatewayBot bot = new DiscordGatewayBot(token);

bool connected = await bot.Connect(client, 0, 1);

if (connected)
{
    bot.AddCommand("!hi", "Hello!");
    bot.AddCommand("!ftw", "DiscordFlat FTW!");
    bot.ListenForCommands();
    
    // Events:
    client.OnPresenceUpdate(PresenceUpdated);
    client.OnGuildCreate(GuildCreated);
    client.OnGuildMemberAdd(GuildMemberAdded);
    client.OnGuildMemberRemove(GuildMemberRemoved);
    client.OnGuildMemberUpdate(GuildMemberUpdated);
    client.OnHeartbeat(HeartbeatAcknowledged);
    client.OnMessage(PostMessage);
}
```
