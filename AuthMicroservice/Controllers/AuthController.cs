using System.ComponentModel.DataAnnotations;
using AuthMicroservice.Model;
using AuthMicroservice.Services.Interfaces;
using AuthMicroservice.Services.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AuthMicroservice.Controllers;

public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly HashingLogic _hashingLogic;
    public AuthController(IUserService userService, HashingLogic hashingLogic)
    {
        _userService = userService;
        _hashingLogic = hashingLogic;
    }

    [HttpPost]
    [Route("register")]
    public ActionResult<User> CreateUser(UserDto userDto)
    {
        try
        {
            var result = _userService.CreateUser(userDto);
            return Created("User/" + result.Id, result);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{userId}")]
    public ActionResult<User> GetUserById(Guid userId)
    {
        try
        {
            return Ok(_userService.GetUserById(userId));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No User found at ID " + userId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    
    [HttpPut]
    [Route("update")]
    public ActionResult<User> UpdateUser(Guid userID, [FromBody] UserDto userDto, string password)
    {
               
        var actualUser = _userService.GetUserById(userID);
        try
        {
            if(!_hashingLogic.ValidateHash(password, actualUser.HashPassword, actualUser.SaltPassword))
            {
                return BadRequest("Wrong password.");
            }
            else
            {
                return Ok(_userService.UpdateUser(actualUser, userDto));
            }
                    
                    
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No User found at ID " + userID);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
}