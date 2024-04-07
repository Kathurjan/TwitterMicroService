using FeedHandlingMicroservice.App;
using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.Controllers;

[ApiController]
public class FeedController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly IHashTagService _hashTagService;
    public FeedController(IPostService postService, IHashTagService hashTagService)
    {
        _postService = postService;
        _hashTagService = hashTagService;
    }


    [HttpPost("CreatePost")]
    public async Task<IActionResult> CreatePost([FromBody] PostDto postDto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (userId == null)
            {
                return BadRequest("User ID is required.");
            }

            // Convert DTO to domain model
            var post = new Post
            {
                UserId = int.Parse(userId),
                Content = postDto.Content,
                CreationDate = DateTime.UtcNow,
                Edited = false, 
                Hashtags = new List<PostHashtag>()
            };

            foreach (var tag in postDto.Hashtags)
            {
                var hashtag = await _hashTagService.FindOrCreateTagAsync(tag); 
                post.Hashtags.Add(new PostHashtag { Hashtag = hashtag });
            }

            await _postService.CreatePost(post); 
            return Ok("Post created successfully.");
        }
        catch (Exception e)
        {
            return BadRequest($"An error occurred: {e.Message}");
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
    public void DeletePost(int id)
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
            var rawId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            
            
            // Check if the "id" claim is present
            if (rawId == null)
            {
                throw new Exception("User ID claim not found.");
            }

            var userId = int.Parse(rawId);


            
            var updatedPost = await _postService.UpdatePost(userId, postId);
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