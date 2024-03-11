
using DTO;
using Entities;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        public Task<List<int>> GetFollowedEntities(int userId);
         public Task GetNotificationsByUserId(int userId);
         public Task<Notification> CreateNotification(NotificationDto notificationDto);
         
         public Task CreateTestUser(int userId);
    }
}
