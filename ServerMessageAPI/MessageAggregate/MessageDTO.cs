namespace ServerMessageAPI.Models;

public class MessageDTO
{
    public int Id { get; set; }
    
    public string Content { get; set; }
    
    public uint UserID { get; set; }
    
    public int ChannelId { get; set; }
}