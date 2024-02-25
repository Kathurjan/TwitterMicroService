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
        _dbContext.Users.Add(user);
        await  _dbContext.SaveChangesAsync();
    }
}