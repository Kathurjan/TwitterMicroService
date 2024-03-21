using Application.Interfaces;
using Entities;
using Infrastructure.IRepositories;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {   


        private readonly INotificationRepo _notificationRepo;
        public NotificationService(INotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }


        public async Task CreateNotification(NotificationDto notificationDto)
        {
            Notification notification = new Notification()
            {
                UserId = notificationDto.UserId,
                Type = notificationDto.Type,
                Message = notificationDto.Message,
                DateOfDelivery = DateTime.Now
            };
            
            await  _notificationRepo.CreateNotification(notification);
            
        }

        public Task<List<string>> GetFollowedEntities(string userId)
        {
            List<string> followedEntities = new List<string>()
            {
                "1", // Example entity ID
                "2", // Example entity ID
                "3"  // Example entity ID
            };

            return Task.FromResult(followedEntities);
        }

        public Task GetNotificationsByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<string>> CreateTestUser(string userId)
        {
            var user = new User()
            {
                Id = userId
            };

            var result = await _notificationRepo.CreateTestUser(user);

            return result;
        }

        public void RebuildDB()
        {
           _notificationRepo.RebuildDB();
        }
    }


}
