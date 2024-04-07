using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using DTO;
using Entities;
using Sharedmodel;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Controllers.UserInteractionController; 

[ApiController]
[Route("api/[controller]")]
public class UserInteractionController : ControllerBase
{

    private readonly INotificationService _notificationService;

    public UserInteractionController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    [HttpPost("SubscribeToNotification")]
    public async Task<ActionResult<string>> SubscribeToNotification(SubscribtionDTO subscribtionDTO)
    {
        try
        {
            var rawId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (rawId == null)
            {
                return BadRequest("User ID is required.");
            }

            var userId = int.Parse(rawId);

            subscribtionDTO.FollowerId = userId.ToString();

            await _notificationService.SubscribeToNotification(subscribtionDTO);
            return Ok("Notification created");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
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
