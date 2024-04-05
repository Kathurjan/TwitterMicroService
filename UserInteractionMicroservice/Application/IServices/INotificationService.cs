
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        public Task<List<string>> GetFollowedEntities(string userId);
        public Task GetNotificationsByUserId(string userId);
        public Task CreateNotification(NotificationDto notificationDto);
         
        public Task<ActionResult<string>> CreateTestUser(string userId);

        public void RebuildDB();
    }
}
