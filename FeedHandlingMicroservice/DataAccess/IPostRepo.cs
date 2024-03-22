using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.DataAccess;

public interface IPostRepo
{
    void CreatePost(Post post);
    
    void RebuildDB();
}