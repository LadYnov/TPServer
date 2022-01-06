using System.ComponentModel.DataAnnotations;
using ServerMessageAPI.Models;
using ServerMessageAPI.UserAggregate;

namespace ServerMessageAPI.MessageAggregate;
public class Message
    {
        [Key]
        public int Id { get; set; }
    
        [MaxLength(200)]
        [Required]
        public string Content { get; set; }
    
        [Required]
        public User User { get; set; }
        
    }
