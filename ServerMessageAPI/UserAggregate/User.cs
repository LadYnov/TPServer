using System.ComponentModel.DataAnnotations;
using ServerMessageAPI.Models;

namespace ServerMessageAPI.UserAggregate;
public class User
{
    [Key]
    public uint Id { get; set; }
    
    [MaxLength(50)]
    public string Username { get; set; }
    
    [MaxLength(50)]
    public string Email { get; set; }
    
    [MaxLength(50)]
    public string Password { get; set; }
    
    public int RoleId { get; set; }
    
    public ICollection<Server> Servers { get; set; }


}