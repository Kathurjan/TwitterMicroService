using System.ComponentModel.DataAnnotations;

namespace AuthMicroservice.Model;

public class User
{   [Key]
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] HashPassword { get; set; }
    public byte[] SaltPassword { get; set; }
}