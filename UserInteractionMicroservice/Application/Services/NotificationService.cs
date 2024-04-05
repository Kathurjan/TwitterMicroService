using Application.Interfaces;
using DTO;
using Entities;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Sockets;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        private NotificationSocket _notificationSocket;
        private readonly INotificationRepo _notificationRepo;

        public NotificationService(INotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public async Task CreateNotification(NotificationDto notificationDto)
        {
            try
            {
                var user = await _notificationRepo.GetUserById(notificationDto.UserId) ?? throw new Exception("User not found");

                Notification notification = new Notification()
                {
                    UserId = notificationDto.UserId,
                    Type = notificationDto.Type,
                    Message = notificationDto.Message,
                    DateOfDelivery = DateTime.Now
                };

                await _notificationRepo.CreateNotification(notification);

                Console.WriteLine(notification.Message);
                await _notificationSocket.SendNotification(notification);
            }
            catch (Exception e)
            {
                // Handle exception
                Console.WriteLine(e.Message);
                throw; // Re-throw the exception to propagate it further if necessary
            }
        }

        public Task<List<string>> GetFollowedEntities(string userId)
        {
            List<string> followedEntities = new List<string>()
            {
                "1", // Example entity ID
                "2", // Example entity ID
                "3" // Example entity ID
            };

            return Task.FromResult(followedEntities);
        }

        public Task GetNotificationsByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult<string>> CreateTestUser(string userId)
        {
            var user = new User() { Id = userId };

            var result = await _notificationRepo.CreateTestUser(user);

            return result;
        }

        public void RebuildDB()
        {
            _notificationRepo.RebuildDB();
        }
    }
}
