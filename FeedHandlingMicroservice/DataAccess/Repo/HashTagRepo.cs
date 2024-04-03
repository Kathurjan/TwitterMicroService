using FeedHandlingMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedHandlingMicroservice.DataAccess;

public class HashTagRepo : IHashTagRepo
{
    private readonly PostDbContext _dbContext;

    public HashTagRepo(PostDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Hashtag> CreateNewHashtag(Hashtag hashtag)
    {
        try
        {
            await _dbContext.Hashtags.AddAsync(hashtag);
            await _dbContext.SaveChangesAsync();
            return hashtag;
        }
        catch (Exception e)
        {
           
            throw new Exception("Something went wrong in Repo CreateNewHashtag method " + e);
        }

    }

    public Task<Hashtag> GetHashTagById(int id)
    {
        
        try
        {
            return _dbContext.Hashtags.FirstOrDefaultAsync(h => h.Id == id)!;
        }
        catch (Exception e)
        {
            
            throw new Exception("Something went wrong in Repo GetHashTagById method " + e);
        }
        
    }

    public Task<List<Hashtag>> GetAllHashTags()
    {
        try
        {
            return _dbContext.Hashtags.ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Something went wrong in Repo GetAllHashTags method " + e);
        }
       
    }

    public async Task<Hashtag> DeleteHashtagById(int id)
    {
        var hashTagToDelete = _dbContext.Hashtags.Find(id) ?? throw new KeyNotFoundException("No such hashtag found");
        _dbContext.Hashtags.Remove(hashTagToDelete);
        await _dbContext.SaveChangesAsync();
        return hashTagToDelete;
    }

    public async Task<Hashtag> FindByTag(string tag)
    {
        var hashtag = await _dbContext.Hashtags.FirstOrDefaultAsync(h => h.Tag == tag);
        return hashtag;
    }
}