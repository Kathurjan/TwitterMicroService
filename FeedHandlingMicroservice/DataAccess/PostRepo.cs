using FeedHandlingMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedHandlingMicroservice.DataAccess;

public class PostRepo : IPostRepo
{
    private readonly PostDbContext _dbContext;
    public PostRepo(PostDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async void CreatePost(Post post)
    {
        try
        {
            _dbContext.PostsTable.Add(post);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("CreatePost in Repo went wrong: " + e);
        }
    }

    public Task<Post> GetPostById(int id)
    {
        return _dbContext.PostsTable.FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<List<Post>> GetAllPost()
    {
        return _dbContext.PostsTable.ToListAsync();
    }

    public Task<List<Post>> GetAllPostByUserId(int userId)
    {

        return _dbContext.PostsTable.Where(p => p.Id == userId).ToListAsync();
    }

    public async void DeletePost(int id)
    {
        var postToDelete = await _dbContext.PostsTable.FindAsync(id);
        if (postToDelete != null)
        {
            _dbContext.PostsTable.Remove(postToDelete);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentException("Post not found", nameof(id));
        }
    }

    public async Task<Post> UpdatePost(Post post)
    {
        var existingPost = await _dbContext.PostsTable.FindAsync(post.Id);
    
        if (existingPost == null)
        {
            throw new ArgumentException("Post not found", nameof(post));
        }
        existingPost.Content = post.Content;
        existingPost.Edited = true;
        
        await _dbContext.SaveChangesAsync();

        return existingPost;
    }



    public void RebuildDB()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }
}