using AuthMicroservice.Model;

namespace AuthMicroservice.Services.Interfaces;

public interface IUserService
{
    Task CreateUser(UserDto userDto);
    public User GetUserById(Guid userId);
    public User UpdateUser(User user, UserDto userDto);
    public User DeleteUser (Guid userId);
    public User GetUserByEmail(string currentUserEmail);
    
    
}