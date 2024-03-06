using AuthMicroservice.Model;

namespace AuthMicroservice.DataAccess.Interfaces;

public interface IUserRepository
{
    Task CreateUser(User user);
    public User GetUserById(Guid userId);
    public User UpdateUser(User user);
    public User DeleteUser (Guid userId);
    public User GetUserByEmail(string currentUserEmail);
  //  public User UserLogin(string username, string password);
}