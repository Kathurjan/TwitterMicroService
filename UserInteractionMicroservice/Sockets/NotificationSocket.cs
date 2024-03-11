using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.OpenApi.Any;

namespace sockets;


public class NotificationSocket : Hub // Simple hub that automatically adds users to groups based on their warehouseId claim
{
    

    //put in Interfaces here 



    public NotificationSocket(
        //instantiate interfaces here
        )
    {
        //inject intefaces here
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        {
            try
            {
                var user = Context.User ?? throw new ApplicationException("User is null");

                if (user.Identity.IsAuthenticated) // Ensures that the user is authenticated
                {
                    var warehouseId = user.FindFirst("warehouseId")?.Value; // Authorizes the user based on their warehouseId claim

                    if (warehouseId != null)
                    {
                        await Groups.AddToGroupAsync(Context.ConnectionId, warehouseId + " InventoryManagement");
                        Console.WriteLine($"Client {Context.ConnectionId} connected to group {warehouseId} in inventory management.");
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in OnConnectedAsync: {e.Message}");
            }
        }

    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            var user = Context.User ?? throw new ApplicationException("User is null");

            if (user!.Identity!.IsAuthenticated)
            {
                var warehouseId = user.FindFirst("warehouseId")?.Value;

                if (warehouseId != null)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, warehouseId + " InventoryManagement");
                    Console.WriteLine($"Client {Context.ConnectionId} disconnected from group {warehouseId} in inventory.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in OnDisconnectedAsync: {e.Message}");
        }

        await base.OnDisconnectedAsync(exception);
    }
}