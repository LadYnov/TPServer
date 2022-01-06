using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerMessageAPI.Models;

namespace ServerMessageAPI.UserAggregate.Port;

public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly ServerMessageContext _context;

    public UserRepository(IMapper mapper, ServerMessageContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public Task<List<UserDTO>> GetUsersAsync()
    {
        return _context.User.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public Task<UserDTO?> GetUserAsync(uint id)
    {
        return _context.User.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> AddUserAsync(UserDTO user)
    {
        var newUser = new User()
        {
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
            RoleId = user.RoleId
        };
        var result = await _context.User.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
}