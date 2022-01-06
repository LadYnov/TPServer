using Microsoft.AspNetCore.Mvc;
using ServerMessageAPI.Models;

namespace ServerMessageAPI.UserAggregate.Port;

public interface IUserRepository
{
    Task<List<UserDTO>> GetUsersAsync();
    Task<UserDTO?> GetUserAsync(uint id);
    Task<User> AddUserAsync(UserDTO user);
}