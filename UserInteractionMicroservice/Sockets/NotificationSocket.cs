using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using UserInteractionMicroservice.Entities;
using UserInteractionMicroservice.Interface;
namespace Sockets;


public class NotificationSocket : Hub
{
    // Assuming you have a service to manage notifications, injected via DI
    private readonly INotificationService _notificationService;

    public NotificationSocket(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            var following = await _notificationService.GetFollowedEntities(userId); // Get entities the user is following
            foreach (var entity in following)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, GenerateGroupName(entity, "following"));
            }
            await base.OnConnectedAsync();
        }
        else
        {
            Console.WriteLine("I am connectamamationgs");
        }
    }

    // Method to associate a connection with a notification group, called after user sends their identifier
    public async Task AssociateWithNewNotificationGroup(string userId)
    {
        Console.WriteLine(userId);
        // Retrieve notification info for the user
        await Groups.AddToGroupAsync(Context.ConnectionId, GenerateGroupName(userId, "following"));
        
    }

    

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        // Similar to OnConnectedAsync, remove the user from their group(s)
        // This requires storing group names or being able to reconstruct them from user info

        await base.OnDisconnectedAsync(exception);
    }


    // Helper method to create a consistent group name
    private string GenerateGroupName(string userUId, string type)
    {
        return $"{userUId}-{type}";
    }
    public async Task SendNotification(Notification notification)
    {
        var groupName = GenerateGroupName(notification.UserUId, notification.Type);
        // Convert the notification object to a format suitable for sending, if necessary
        // For simplicity, we'll assume you can send the object directly
        await Clients.Group(groupName).SendAsync("ReceiveNotification", notification);
    }
}
