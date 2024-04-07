using Application.Interfaces;
using Entities;
using DTO;
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

                var user = await _notificationRepo.GetUserById(notificationDto.UserId) ?? throw new Exception("User not found");

                Notification notification = new Notification()
                {
                    CreatorId = notificationDto.UserId,
                    Type = notificationDto.Type,
                    Message = notificationDto.Message,
                    DateOfDelivery = DateTime.Now
                };

                await _notificationRepo.CreateNotification(notification);

                var groupName = $"{notification.CreatorId}-{notification.Type}";
                Console.WriteLine(groupName);
                await _hubContext.Clients.Group(groupName).SendAsync("ReceiveNotification", notificationDto);
            }
            catch (Exception e)
            {
                // Handle exception
                Console.WriteLine(e.Message);
                throw; // Re-throw the exception to propagate it further if necessary
            }
        }

        public async Task<List<Subscriptions>> GetSubscriptions(string userId)
        {
            var subscriptions = await _notificationRepo.GetSubscriptions(userId);

            return subscriptions;
        }

        public async Task<ActionResult<string>> CreateTestUser(string userId)
        {
            var user = new User() { Id = userId };

            var result = await _notificationRepo.CreateTestUser(user);

            return result;
        }

        public async Task<ActionResult<string>> SubscribeToNotification(SubscribtionDTO subscribtionDTO)
        {

            if(subscribtionDTO.FollowerId == subscribtionDTO.CreatorId && subscribtionDTO.Type != "FriendRequest")
            {
                return "You can't subscribe to yourself";
            }

            var notifcationRelation = new Subscriptions()
            {
                CreatorId = subscribtionDTO.CreatorId,
                FollowerId = subscribtionDTO.FollowerId,
                Type = subscribtionDTO.Type
            };

            await _notificationRepo.CreateSubscription(notifcationRelation);

            return "Subscribed";
        }



        public void RebuildDB()
        {
            _notificationRepo.RebuildDB();
        }
    }
}
