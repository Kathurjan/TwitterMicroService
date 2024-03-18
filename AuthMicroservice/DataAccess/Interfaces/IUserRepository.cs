using AuthMicroservice.Model;

namespace AuthMicroservice.DataAccess.Interfaces;

public interface IUserRepository
{
    User CreateUser(User user);
    public User GetUserById(int userId);
    public User UpdateUser(User user);
    public User DeleteUser (int userId);
    public User GetUserByEmail(string currentUserEmail);
  //  public User UserLogin(string username, string password);
    void RebuildDB();
}