using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using DTO;



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
