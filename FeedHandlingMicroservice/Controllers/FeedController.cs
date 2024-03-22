using System.Security.Claims;
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
    public Task<Post>? CreatePost(string content)
    {
        try
        {
            // Access the current user's claims
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            // Check if the "id" claim is present
            if (userId == null)
            {
                throw new Exception("User ID claim not found.");
            }
    
            PostDto postDto = new PostDto();
            postDto.UserId = int.Parse(userId);
        
            if (string.IsNullOrEmpty(content))
            {
                throw new Exception("Content is null or empty.");
            }

            postDto.Content = content;

            if (_postService == null)
            {
              
                throw new Exception("_postService is null.");
            }

            _postService.CreatePost(postDto);

            // Return whatever is appropriate for your scenario
            return null;
        }
        catch (Exception e)
        {
            
            throw;
        }
    }

    
    [HttpPost]
    [Route("RebuildDB")]
    public void RebuildDB()
    {
        _postService.RebuildDB();
    }
}