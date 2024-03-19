
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        public Task<List<int>> GetFollowedEntities(int userId);
         public Task GetNotificationsByUserId(int userId);
         public Notification CreateNotification(NotificationDto notificationDto);
         
         public Task<ActionResult<string>> CreateTestUser(int userId);

         public void RebuildDB();
    }
}
