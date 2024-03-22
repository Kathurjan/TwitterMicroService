using FeedHandlingMicroservice.App;
using FeedHandlingMicroservice.Models;
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
    public async Task<IActionResult> CreatePost(string content)
    {
        try
        {
            // Access the current user's claims
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            
            // Check if the "id" claim is present
            if (userId == null)
            {
                throw new Exception("User ID claim not found.");
            }

            PostDto postDto = new PostDto {
                UserId = int.Parse(userId),
                Content = content
            };
            
             await _postService.CreatePost(postDto);
            return Ok("Post created successfully.");
        }
        catch (Exception e)
        {

            throw new Exception("Controller CreatePost method went wrong" + e);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPostById(int id)
    {
        try
        {
            var post = await _postService.GetPostById(id);
            return post;
        }
        catch (Exception e)
        {
            throw new Exception("Controller GetPostById method went wrong" + e);
        }
    }
    
    [HttpGet("GetAllPosts")]
    public async Task<ActionResult<List<Post>>> GetAllPosts()
    {
        try
        {
            var posts = await _postService.GetAllPost();
            return posts;
        }
        catch (Exception e)
        {
            throw new Exception("Controller GetAllPosts method went wrong" + e);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Post>>> GetAllPostsByUserId(int userId)
    {
        try
        {
            var posts = await _postService.GetAllPostByUserId(userId);
            return posts;
        }
        catch (Exception e)
        {
            throw new Exception("Controller GetAllPostsByUserId method went wrong" + e);
        }
    }
    
    [HttpDelete("{id}")]
    public Task<IActionResult> DeletePost(int id)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            
            // Check if the "id" claim is present
            if (userId == null)
            {
                throw new Exception("User ID claim not found.");
            }
            
            _postService.DeletePost(id);
            return Task.FromResult<IActionResult>(NoContent());
        }
        catch (Exception e)
        {
            throw new Exception("Controller DeletePost method went wrong" + e);
        }
    }
    
    [HttpPut("{postId}")]
    public async Task<ActionResult<Post>> UpdatePost(string content, int postId)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            
            // Check if the "id" claim is present
            if (userId == null)
            {
                throw new Exception("User ID claim not found.");
            }

            PostDto postDto = new PostDto {
                UserId = int.Parse(userId),
                Content = content
            };
            
            var updatedPost = await _postService.UpdatePost(postDto, postId);
            return updatedPost;
        }
        catch (Exception e)
        {
            throw new Exception("Controller UpdatePost method went wrong" + e);
        }
    }
    [HttpPost]
    [Route("RebuildDB")]
    public void RebuildDB()
    {
        _postService.RebuildDB();
    }
}