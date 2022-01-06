using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerMessageAPI.MessageAggregate;
using ServerMessageAPI.Models;
using ServerMessageAPI.Models.Port;

namespace ServerMessageAPI.ServerAggregate.Port;

public class ServerRepository : IServerRepository
{
    private readonly IMapper _mapper;
    private readonly ServerMessageContext _context;

    public ServerRepository(IMapper mapper, ServerMessageContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public Task<List<ServerDTO>> GetServerAsync()
    {
        return _context.Message.ProjectTo<ServerDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public Task<ServerDTO?> GetServerAsync(int id)
    {
        return _context.Message.ProjectTo<ServerDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(server => server.Id == id);
    }

    public async Task<Server> AddServerAsync(ServerDTO serverDto)
    {
        var newServer = new Server()
        {
            Name = serverDto.Name,
            OwnerId = serverDto.OwnerId
        };
        var result = await _context.Server.AddAsync(newServer);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ActionResult<Server>> UpdateServer(Server server)
    {
        var result = await _context.Server.FirstOrDefaultAsync(s => s.Id == server.Id);
        
        if (result == null) return null;
        
        result.Name = server.Name;
        result.OwnerId = server.OwnerId;
        result.Users = server.Users;
        result.Channels = server.Channels;
        await _context.SaveChangesAsync();
        return result;

    }

    public async Task<Server> GetServerByName(string name)
    {
        return await _context.Server.FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task<ActionResult<Server>> DeleteServerAsync(int id)
    {
        var result = await _context.Server.FirstOrDefaultAsync(server => server.Id == id);
        if (result == null) return null;
        _context.Server.Remove(result);
        await _context.SaveChangesAsync();
        return result;
    }
}