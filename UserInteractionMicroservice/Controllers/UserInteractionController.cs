using Microsoft.AspNetCore.Mvc;
using RabbitMq.RabbitMqIServices;

namespace Controllers.UserInteractionController; 

[ApiController]
[Route("api/[controller]")]
public class UserInteractionController : ControllerBase
{
    private readonly IRabbitMqSender _rabbitMqSender;

    public UserInteractionController(IRabbitMqSender rabbitMqSender)
    {
        _rabbitMqSender = rabbitMqSender;
    }
    
    [HttpPost("SendTest")]
    public void ConsumeUserInteraction(TestInteraction testInteraction)
    {
        Console.WriteLine("UserInteractionController: " + testInteraction.Message);
        _rabbitMqSender.Send(testInteraction.Message);
    }
}
