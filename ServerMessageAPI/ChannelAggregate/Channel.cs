using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServerMessageAPI.MessageAggregate;

namespace ServerMessageAPI.Models;

public class Channel
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string Name { get; set; }
    
    public Server Server { get; set; }
    
    public ICollection<Message> Messages { get; set; }
}