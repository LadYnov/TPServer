using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ServerMessageAPI.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    
    public string NameRole { get; set; }
}