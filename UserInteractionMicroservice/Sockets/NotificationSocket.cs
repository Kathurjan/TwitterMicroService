using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Entities;
using Application.Interfaces;
using DTO;
namespace Sockets;


public class NotificationSocket : Hub
{
    private readonly IHubContext<NotificationSocket> _notificationSocket;
    private readonly INotificationService _notificationService;

    public NotificationSocket(
        INotificationService notificationService, 
        IHubContext<NotificationSocket> notificationSocket
    )
    {
        _notificationSocket = notificationSocket; 
        _notificationService = notificationService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (!string.IsNullOrEmpty(userId)) // Check if userId is not null or empty
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
            Console.WriteLine("Invalid user identifier format or user identifier is null");
        }
    }

    // Method to associate a connection with a notification group, called after user sends their identifier
    public async Task FollowNotificationGroup(string userId)
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
    private string GenerateGroupName(string userId, string type)
    {
        return $"{userId}-{type}";
    }
    public async Task SendNotification(Notification notification)
    {
        var groupName = GenerateGroupName(notification.UserId, notification.Type);
        /* await Clients.Group(groupName).SendAsync("ReceiveNotification", notification); */
        Console.WriteLine(notification.Message+ " Sent to notification group: "+ groupName);
    }
}
