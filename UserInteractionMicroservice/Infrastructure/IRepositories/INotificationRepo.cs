using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.IRepositories;

public interface INotificationRepo
{
    Task CreateNotification(Notification notification);
    Task<Notification> GetNotificationById(int id);
    Task<List<Notification>> GetNotificationsByUserId(string userId);
    Task<Notification> UpdateNotification(Notification notification);
    Task<bool> DeleteNotification(int id);

    Task<User> GetUserById(string userId);

    Task<ActionResult<string>> CreateTestUser(User user);

    void RebuildDB();
}