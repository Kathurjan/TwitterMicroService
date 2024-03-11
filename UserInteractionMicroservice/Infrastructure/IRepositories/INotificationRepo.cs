using Entities;

namespace Infrastructure.IRepositories;

public interface INotificationRepo
{
    Task<Notification> CreateNotification(Notification notification);
    Task<Notification> GetNotificationById(int id);
    Task<List<Notification>> GetNotificationsByUserId(int userId);
    Task<Notification> UpdateNotification(Notification notification);
    Task<bool> DeleteNotification(int id);
}