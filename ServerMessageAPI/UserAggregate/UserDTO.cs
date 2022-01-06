﻿using ServerMessageAPI.Models;

namespace ServerMessageAPI.UserAggregate;

public class UserDTO
{
    public uint Id { get; set; }
    
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public int RoleId { get; set; }
    

}