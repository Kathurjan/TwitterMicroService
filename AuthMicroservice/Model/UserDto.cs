using System.ComponentModel.DataAnnotations;

namespace AuthMicroservice.Model;

public class UserDto
{ 
    public string Username { get; set; }
    public string Email { get; set; }
    public string password { get; set; }
}