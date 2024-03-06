using AuthMicroservice.DataAccess.Interfaces;
using AuthMicroservice.Model;

namespace AuthMicroservice.DataAccess.Repositories;

public class UserRepository : IUserRepository
{

    private readonly DbContext _dbContext;

    public UserRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task CreateUserSync(User user)
    {
        if (_dbContext.Users.Any(u =>u.Email.Equals(user.Email)))
        {
            throw new Exception("User with this email already exists");
        }
        _dbContext.Users.Add(user);
        await  _dbContext.SaveChangesAsync();
    }

    public Task<User> GetUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async  Task DeleteUserAsync(int userId)
    {
        var userToDelete = await _dbContext.Users.FindAsync(userId);

        if (userToDelete !=null)
        {
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public Task<User> ValidateUserLoginAsync(string username, string password)
    {
        throw new NotImplementedException();
    }
}