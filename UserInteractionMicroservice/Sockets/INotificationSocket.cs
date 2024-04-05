
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace Application.Interfaces
{
    public interface INotificationSocket
    {
        public Task AssociateWithNewNotificationGroup(string userId);

        public Task SendNotification(NotificationDto notification);
    }
}
