using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.App;

public interface IPostService
{
    void CreatePost(PostDto postDto);
    
    void RebuildDB();
}