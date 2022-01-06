using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerMessageAPI.Models;

namespace ServerMessageAPI.ChannelAggregate.Port;

public class ChannelRepository : IChannelRepository
{
    private readonly IMapper _mapper;
    private readonly ServerMessageContext _context;

    public ChannelRepository(IMapper mapper, ServerMessageContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<List<ChannelDTO>> GetChannelAsync()
    {
        return await _context.Channel.ProjectTo<ChannelDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ChannelDTO?> GetChannelAsync(int id)
    {
        return await _context.Channel.ProjectTo<ChannelDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(channel => channel.Id == id);
    }

    public Task<Channel> AddChannelAsync(ChannelDTO channel)
    {
        throw new NotImplementedException();
    }

    public async Task<Channel> DeleteChannelAsync(int id)
    {
        var result = await _context.Channel.FirstOrDefaultAsync(channel => channel.Id == id);
        if (result == null) return null;
        _context.Channel.Remove(result);
        await _context.SaveChangesAsync();
        return result;

    }

    public async Task<Channel> UpdateChannel(Channel channel)
    {
        var result = await _context.Channel.FirstOrDefaultAsync(c => c.Id == channel.Id);
        if (result != null)
        {
            result.Name = channel.Name;
            result.Server = channel.Server;
            result.Messages = channel.Messages;
            await _context.SaveChangesAsync();
            return result;
        }

        return null;
    }

    public async Task<Channel> GetChannelByName(string name)
    {
        return await _context.Channel.FirstOrDefaultAsync(c => c.Name == name);
    }
}