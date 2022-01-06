using ServerMessageAPI.MessageAggregate;

namespace ServerMessageAPI.Models.Port;

public interface IMessageRepository
{
    Task<List<MessageDTO>> GetMessageAsync();
    Task<MessageDTO?> GetMessageAsync(int id);
    Task<Message> AddMessageAsync(Message message);
}