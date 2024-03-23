using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.DataAccess;

public interface IPostRepo
{
    Task<Post> CreatePost(Post post);
    Task<Post> GetPostById(int id);
    Task<List<Post>> GetAllPost();
    Task<List<Post>> GetAllPostByUserId(int userId);
    Task<Post> DeletePost(int id);
    Task<Post> UpdatePost(Post post);
    
    void RebuildDB();
}