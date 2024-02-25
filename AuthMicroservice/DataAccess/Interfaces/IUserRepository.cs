using AuthMicroservice.Model;

namespace AuthMicroservice.DataAccess.Interfaces;

public interface IUserRepository
{
    Task CreateUserSync(User user);
    
}