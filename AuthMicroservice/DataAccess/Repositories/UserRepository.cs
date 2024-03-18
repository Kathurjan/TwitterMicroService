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
    
    public  User CreateUser(User user)
    {
        if (_dbContext.UsersTable.Any(u =>u.Email.Equals(user.Email)))
        {
            throw new Exception("User with this email already exists");
        }
        _dbContext.UsersTable.Add(user);
        _dbContext.SaveChangesAsync();
        return user;
    }

    public User GetUserById(int userId)
    { 
        var userToFind = _dbContext.UsersTable.FirstOrDefault(u => u.Id == (userId));
        return userToFind;
    }
    
    public User UpdateUser(User user)
    {
        _dbContext.UsersTable.Update(user);
        _dbContext.SaveChanges();
        return user;
    }
    
    public User DeleteUser(int userId)
    {
        var userToDelete =  _dbContext.UsersTable.FirstOrDefault(u => u.Id == (userId));

        if (userToDelete !=null)
        {
             _dbContext.UsersTable.Remove(userToDelete);
             _dbContext.SaveChangesAsync();
        }
        // returning deleted user for now. may need to change //TODO
        return userToDelete;
    }


    
    public User GetUserByEmail(string userEmail)
    {
        
        return _dbContext.UsersTable.FirstOrDefault(u => u.Email == userEmail)!;;
    }

    
    public void RebuildDB()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }
}