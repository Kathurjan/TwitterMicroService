using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.App;

public interface IPostService
{
    void CreatePost(PostDto postDto);
    Task<Post> GetPostById(int id);
    Task<List<Post>> GetAllPost();
    Task<List<Post>> GetAllPostByUserId(int userId);
    void DeletePost(int id);
    Task<Post> UpdatePost(PostDto postDto);
    void RebuildDB();
}