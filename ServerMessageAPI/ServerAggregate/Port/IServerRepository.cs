using Microsoft.AspNetCore.Mvc;
using ServerMessageAPI.Models;

namespace ServerMessageAPI.ServerAggregate.Port;

public interface IServerRepository
{
    Task<List<ServerDTO>> GetServerAsync();
    Task<ServerDTO?> GetServerAsync(int id);
    Task<Server> AddServerAsync(ServerDTO serverDto);
    Task<ActionResult<Server>> UpdateServer(Server server);
    Task<Server> GetServerByName(string serverDtoName);
    Task<ActionResult<Server>> DeleteServerAsync(int id);
}