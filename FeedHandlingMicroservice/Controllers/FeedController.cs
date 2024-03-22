using FeedHandlingMicroservice.App;
using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.Controllers;

[ApiController]
public class FeedController : ControllerBase
{
    private readonly IPostService _postService;
    public FeedController(IPostService postService)
    {
        _postService = postService;
    }


    [HttpPost("CreatePost")]
    [Authorize]
    public Task<Post>? CreatePost([FromBody] PostDto postDto)
    {
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            var userId = int.Parse(userIdClaim.Value);
            postDto.UserId = userId;
            _postService.CreatePost(postDto);
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Controller went wrong" + e);
        }
    }
    
    
    
    
    
    [HttpPost]
    [Route("RebuildDB")]
    public void RebuildDB()
    {
        _postService.RebuildDB();
    }
}