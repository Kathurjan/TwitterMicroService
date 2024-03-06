using AuthMicroservice.Model;

namespace AuthMicroservice.DataAccess.Interfaces;

public interface IUserRepository
{
    Task CreateUser(User user);
    public User GetUserById(Guid userId);
    public User UpdateUser(User user);
    Task DeleteUserAsync(Guid userId);
    Task<User> ValidateUserLoginAsync(string username, string password);
}