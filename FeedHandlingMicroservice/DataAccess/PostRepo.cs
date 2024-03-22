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

    public async Task<Post> CreatePost(Post post)
    {
        try
        {
            await _dbContext.PostsTable.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return post;
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

        return _dbContext.PostsTable.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<Post> DeletePost(int id)
    {
        var postToDelete = GetPostById(id);
        if (postToDelete != null)
        {
            _dbContext.PostsTable.Remove(await postToDelete);
            await _dbContext.SaveChangesAsync();
            return await postToDelete;
        }

        throw new ArgumentException("Post not found", nameof(id));
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