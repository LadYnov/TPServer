using Microsoft.EntityFrameworkCore;
using ServerMessageAPI.MessageAggregate;
using ServerMessageAPI.UserAggregate;

namespace ServerMessageAPI.Models;

public class ServerMessageContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Channel> Channel { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<Server> Server { get; set; }
    public DbSet<Role> Role { get; set; }
    
    public ServerMessageContext(DbContextOptions<ServerMessageContext> options) : base(options){}
}