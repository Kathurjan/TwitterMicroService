using FeedHandlingMicroservice.App;
using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.Controllers;


[ApiController]
public class HashTagController  : ControllerBase
{
    private readonly IHashTagService _hashTagService;
    
    public HashTagController(IHashTagService hashTagService)
    {
        _hashTagService = hashTagService;
    }
    
    
    
    
    [HttpPost("CreateHashTag")]
    public async Task<IActionResult> CreateHashTag(HashtagDto hashtagDto)
    {
        try
        {
            
            await _hashTagService.CreateNewHashtag(hashtagDto);
            return Ok("HashTag created successfully.");
        }
        catch (Exception e)
        {

            throw new Exception("Controller CreateHashTag method went wrong" + e);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Hashtag>> GetHashTagById(int id)
    {
        try
        {
            var post = await _hashTagService.GetHashTagById(id);
            return post;
        }
        catch (Exception e)
        {
            throw new Exception("Controller GetHashTagById method went wrong" + e);
        }
    }
    
    [HttpGet("GetAllHashTags")]
    public async Task<ActionResult<List<Hashtag>>> GetAllHashTags()
    {
        try
        {
            var posts = await _hashTagService.GetAllHashTags();
            return posts;
        }
        catch (Exception e)
        {
            throw new Exception("Controller GetAllHashTags method went wrong" + e);
        }
    }
    
    [HttpDelete("{id}")]
    public void DeleteHashTag(int id)
    {
        try
        {
            _hashTagService.DeleteHashtagById(id);
        }
        catch (Exception e)
        {
            throw new Exception("Controller DeleteHashTag method went wrong" + e);
        }
    }
}