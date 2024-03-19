using Microsoft.AspNetCore.Mvc;
using RabbitMq.RabbitMqIServices;
using Application.Interfaces;
using DTO;

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
    
    [HttpPost("SendTest")]
    public void ConsumeUserInteraction(TestInteraction testInteraction)
    {
        Console.WriteLine("UserInteractionController: " + testInteraction.Message);
        _rabbitMqSender.Send(testInteraction);
    }

    [HttpPost("NotificationTest")]
    public void CreateNotification(NotificationDto notificationDto)
    {
        _notificationService.CreateNotification(notificationDto);
    }

    [HttpPost("CreateTestUser")]
    public async void CreateTestUser(int userId)
    {
        await _notificationService.CreateTestUser(userId);
    }

    [HttpPost("RebuildDB")]
    public void RebuildDB()
    {
       _notificationService.RebuildDB();
    }
   


}
