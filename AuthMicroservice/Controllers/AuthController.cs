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
    private readonly Authentication _authentication;
    public AuthController(IUserService userService, HashingLogic hashingLogic, Authentication authentication)
    {
        _userService = userService;
        _hashingLogic = hashingLogic;
        _authentication = authentication;
        
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
    public ActionResult<User> UpdateUser(Guid userId, [FromBody] UserDto userDto, string password)
    {
               
        var actualUser = _userService.GetUserById(userId);
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
            return NotFound("No User found at ID " + userId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
    [HttpDelete]
    [Route("delete")]
    public ActionResult<User> DeleteUserById(Guid userId)
    {
        try
        {
            return Ok(_userService.DeleteUser(userId));
        }
        catch (KeyNotFoundException e)
        {
            return NotFound("No specification found at ID " + userId);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.ToString());
        }
    }
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto userDto)
    {
        try
        {
            var currentUser = _userService.GetUserByEmail(userDto.Email.ToLower());
            if(currentUser == null)
                return BadRequest("Wrong password or Email.");
            if (currentUser.Email != userDto.Email.ToLower())
            {
                return BadRequest("Wrong password or Email.");
            }

            if (!_hashingLogic.ValidateHash(userDto.password, currentUser.HashPassword, currentUser.SaltPassword))
            {
                return BadRequest("Wrong password or Email.");
            }

            string token = _authentication.CreateToken(currentUser);
            return Ok(token);
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
    
}