using System.Security.Claims;
using System.Threading.Tasks.Dataflow;
using Application.Interfaces;
using DTO;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

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
        await base.OnConnectedAsync();
    }

    // Method to associate a connection with a notification group, called after user sends their identifier
    public async Task Subscribe(SubscribtionDTO subscribtionDTO)
    {
        try
        {
            await _notificationService.SubscribeToNotification(subscribtionDTO);
            var groupName = GenerateGroupName(subscribtionDTO.CreatorId, subscribtionDTO.Type);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine("Added to group: " + groupName);
        }
        catch (Exception)
        {
            Console.WriteLine("Failed to subscribe to notification group");
        }
    }

    public async Task GetSubscription(string userId)
    {
        Console.WriteLine("Getting subscriptions for user: " + userId);
        var subscriptions = await _notificationService.GetSubscriptions(userId);

        foreach (var subscription in subscriptions)
        {
            var groupName = GenerateGroupName(subscription.CreatorId, subscription.Type);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            Console.WriteLine("Added to group: " + groupName);
        }
        await Clients.Caller.SendAsync("ReceiveSubscriptions", subscriptions);
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

    public void SendNotification(Notification notification)
    {
        var groupName = GenerateGroupName(notification.CreatorId, notification.Type);
        /* await Clients.Group(groupName).SendAsync("ReceiveNotification", notification); */
        Console.WriteLine(notification.Message + " Sent to notification group: " + groupName);
    }
}
