using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServerMessageAPI.UserAggregate;

namespace ServerMessageAPI.Models;
public class Server
{
 [Key]
 public int Id { get; set; }
 
 [MaxLength(50)]
 public string Name { get; set; }
 
 [ForeignKey("User")]
 public int OwnerId { get; set; }

 public ICollection<User> Users { get; set; }
 
 public ICollection<Channel> Channels { get; set; }
}