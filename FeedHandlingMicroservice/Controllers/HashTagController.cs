using FeedHandlingMicroservice.App;
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
    
}