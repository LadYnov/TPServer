using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ServerMessageAPI.MessageAggregate;

namespace ServerMessageAPI.Models.Port;

public class MessageRepository : IMessageRepository
{
    private readonly IMapper _mapper;
    private readonly ServerMessageContext _context;

    public MessageRepository(IMapper mapper, ServerMessageContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public Task<List<MessageDTO>> GetMessageAsync()
    {
        return _context.Message.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public Task<MessageDTO?> GetMessageAsync(int id)
    {
        return _context.Message.ProjectTo<MessageDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(message => message.Id == id);
    }

    public Task<Message> AddMessageAsync(Message message)
    {
        throw new NotImplementedException();
    }
}