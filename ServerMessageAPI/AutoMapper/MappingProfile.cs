using AutoMapper;
using ServerMessageAPI.ChannelAggregate;
using ServerMessageAPI.MessageAggregate;
using ServerMessageAPI.Models;
using ServerMessageAPI.UserAggregate;

namespace ServerMessageAPI.AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        
            CreateMap<Message, MessageDTO>();
            CreateMap<MessageDTO, Message>();
            
            CreateMap<Channel, ChannelDTO>();
            CreateMap<ChannelDTO, Channel>();
        }
    }
