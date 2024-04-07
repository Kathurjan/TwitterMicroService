using Application.Interfaces;
using Entities;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Sharedmodel;
using Sockets;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
         private readonly IHubContext<NotificationSocket> _hubContext;
        private readonly INotificationRepo _notificationRepo;

        public NotificationService(INotificationRepo notificationRepo, IHubContext<NotificationSocket> hubContext)
        {
            _notificationRepo = notificationRepo;
            _hubContext = hubContext;
        }

        public async Task CreateNotification(NotificationDto notificationDto)
        {
            try
            {
                Console.WriteLine("Creating notification");
                Console.WriteLine(notificationDto.Message);
                Console.WriteLine(notificationDto.UserId);
                Console.WriteLine(notificationDto.Type);
                
                var user = await _notificationRepo.GetUserById(notificationDto.UserId) ?? throw new Exception("User not found");

                Notification notification = new Notification()
                {
                    UserId = notificationDto.UserId,
                    Type = notificationDto.Type,
                    Message = notificationDto.Message,
                    DateOfDelivery = DateTime.Now
                };

                await _notificationRepo.CreateNotification(notification);

                var groupName = $"{notification.Id}-{notification.Type}";
                await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", notificationDto);
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
