using Microsoft.AspNetCore.Mvc;
using RabbitMq.RabbitMqIServices;
using Application.Interfaces;
using DTO;
using Entities;

namespace Controllers.UserInteractionController; 

[ApiController]
[Route("api/[controller]")]
public class UserInteractionController : ControllerBase
{
    private readonly IRabbitMqSender _rabbitMqSender;

    private readonly INotificationService _notificationService;

    public UserInteractionController(IRabbitMqSender rabbitMqSender, INotificationService notificationService)
    {
        _rabbitMqSender = rabbitMqSender;
        _notificationService = notificationService;
    }
    
    [HttpPost("Notify")]
    public void ConsumeNotification(NotificationDto notification)
    {
        Console.WriteLine("UserInteractionController: " + notification.Message);
        _notificationService.CreateNotification(notification);
    }

    [HttpPost("NotificationTest")]
    public void CreateNotification(NotificationDto notificationDto)
    {
        _notificationService.CreateNotification(notificationDto);
    }

    [HttpPost("CreateTestUser")]
    public async Task<ActionResult<string>> CreateTestUser(UserCreationDTO userCreationDTO)
    {
        var result = await _notificationService.CreateTestUser(userCreationDTO.UserId);

        return Ok(result);
    }

    [HttpPost("RebuildDB")]
    public void RebuildDB()
    {
       _notificationService.RebuildDB();
    }
   


}
