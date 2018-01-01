# DiscordFlat
.NET wrapper for Discord's API (https://discordapp.com/developers/docs/intro)

## How to use:
### WebSocket client:
```
DiscordWebSocketClient client = new DiscordWebSocketClient();
DiscordGatewayBot bot = new DiscordGatewayBot("token");

bool connected = await bot.Connect(client, shardId, shardCount);

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

### OAuth2 Client:
```
DiscordClient client = new DiscordClient();
TokenResponse token = client.GetAccessToken("clientId", "clientSecret", "accessCode", "redirectUri");

// Guild Manager example:
IDiscordGuildManager manager = new GuildManager();
GuildMembers members = manager.GetMembers(token, "guildId", 1000);
GuildRoles roles = manager.GetRoles(token, "guildId");
GuildRole role = manager.GetRole(roles, "Trial");

bool added = manager.AddUserToRole(token, "guildId", "userId", "roleId");
bool removed = manager.RemoveRoleFromUser(token, "guildId", "userId", "roleId");
```
