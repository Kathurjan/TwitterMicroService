using FeedHandlingMicroservice.Models;

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
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("CreatePost in Repo went wrong: " + e);
        }
    }
    
    
    public void RebuildDB()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }
}