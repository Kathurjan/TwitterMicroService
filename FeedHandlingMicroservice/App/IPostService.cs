using FeedHandlingMicroservice.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHandlingMicroservice.App;

public interface IPostService
{
    Task<Post> CreatePost(PostDto postDto);
    Task<Post> GetPostById(int id);
    Task<List<Post>> GetAllPost();
    Task<List<Post>> GetAllPostByUserId(int userId);
    Task<Post> DeletePost(int id);
    Task<Post> UpdatePost(PostDto postDto, int postId);
    void RebuildDB();
}