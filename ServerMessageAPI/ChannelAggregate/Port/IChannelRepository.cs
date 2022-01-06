using Microsoft.AspNetCore.Mvc;
using ServerMessageAPI.MessageAggregate;
using ServerMessageAPI.Models;

namespace ServerMessageAPI.ChannelAggregate.Port;

public interface IChannelRepository
{
    Task<List<ChannelDTO>> GetChannelAsync();
    Task<ChannelDTO?> GetChannelAsync(int id);
    Task<Channel> AddChannelAsync(ChannelDTO channel);
    Task<Channel> DeleteChannelAsync(int id);
    Task<Channel> UpdateChannel(Channel channel);

    Task<Channel> GetChannelByName(string name);
}