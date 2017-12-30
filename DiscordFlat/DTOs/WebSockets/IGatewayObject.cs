namespace DiscordFlat.DTOs.WebSockets
{
    public interface IGatewayObject
    {
        string EventName { get; set; }
        int? OpCode { get; set; }
        int? SequenceNumber { get; set; }
    }
}