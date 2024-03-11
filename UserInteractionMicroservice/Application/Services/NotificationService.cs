using Application.Interfaces;
using Entities;
using Infrastructure.IRepositories;
using DTO;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {   


        private readonly INotificationRepo _notificationRepo;
        public NotificationService(INotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }


        public async Task<Notification> CreateNotification(NotificationDto notificationDto)
        {
            Notification notification = new Notification()
            {
                UserId = notificationDto.UserId,
                Type = notificationDto.Type,
                Message = notificationDto.Message,
                DateOfDelivery = DateTime.Now
            };

            await _notificationRepo.CreateNotification(notification);

            return notification;
        }

        public Task<List<int>> GetFollowedEntities(int userId)
        {
            List<int> followedEntities = new List<int>()
            {
                1, // Example entity ID
                2, // Example entity ID
                3  // Example entity ID
            };

            return Task.FromResult(followedEntities);
        }

        public Task GetNotificationsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateTestUser(int userId)
        {
            var user = new User()
            {
                Id = userId
            };

            await _notificationRepo.CreateTestUser(user);
        }
    }
}