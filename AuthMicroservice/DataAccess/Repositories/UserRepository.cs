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
    
    public async Task CreateUser(User user)
    {
        if (_dbContext.UsersTable.Any(u =>u.Email.Equals(user.Email)))
        {
            throw new Exception("User with this email already exists");
        }
        _dbContext.UsersTable.Add(user);
        await  _dbContext.SaveChangesAsync();
    }

    public User GetUserById(Guid userId)
    { 
        var userToFind = _dbContext.UsersTable.FirstOrDefault(u => u.Id.Equals(userId));
        return userToFind;
    }
    
    public User UpdateUser(User user)
    {
        _dbContext.UsersTable.Update(user);
        _dbContext.SaveChanges();
        return user;
    }
    
    public async  Task DeleteUserAsync(Guid userId)
    {
        var userToDelete = await _dbContext.UsersTable.FindAsync(userId);

        if (userToDelete !=null)
        {
            _dbContext.UsersTable.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }

    public Task<User> ValidateUserLoginAsync(string username, string password)
    {
        throw new NotImplementedException();
    }
}