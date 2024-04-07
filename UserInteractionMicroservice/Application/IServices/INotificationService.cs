using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Sharedmodel;


namespace Application.Interfaces
{
    public interface INotificationService
    {
        public Task<List<Subscriptions>> GetSubscriptions(string userId);
        public Task CreateNotification(NotificationDto notificationDto);
         
        public Task<ActionResult<string>> CreateTestUser(string userId);

        public Task<ActionResult<string>> SubscribeToNotification(SubscribtionDTO subscribtionDTO);

        public void RebuildDB();
    }
}
