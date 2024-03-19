using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.IRepositories;

public interface INotificationRepo
{
    Notification CreateNotification(Notification notification);
    Task<Notification> GetNotificationById(int id);
    Task<List<Notification>> GetNotificationsByUserId(int userId);
    Task<Notification> UpdateNotification(Notification notification);
    Task<bool> DeleteNotification(int id);

    Task<ActionResult<string>> CreateTestUser(User user);

    void RebuildDB();
}