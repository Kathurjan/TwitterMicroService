using AuthMicroservice.Model;

namespace AuthMicroservice.DataAccess.Interfaces;

public interface IUserRepository
{
    Task CreateUserSync(User user);
    Task<User> GetUserAsync(int userId);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(int userId);
    Task<User> ValidateUserLoginAsync(string username, string password);
}