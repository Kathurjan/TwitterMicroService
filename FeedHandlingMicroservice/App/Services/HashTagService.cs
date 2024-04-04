using AutoMapper;
using FeedHandlingMicroservice.DataAccess;
using FeedHandlingMicroservice.Models;

namespace FeedHandlingMicroservice.App;

public class HashTagService : IHashTagService
{
    
    private readonly IMapper _mapper;
    private readonly IHashTagRepo _hashTagRepo;
    public HashTagService(IMapper mapper, IHashTagRepo hashTagRepo)
    {
        _mapper = mapper;
        _hashTagRepo = hashTagRepo;
    }

    public Task<Hashtag> CreateNewHashtag(HashtagDto hashtagDto)
    {
        try
        {
            var hashTag = _mapper.Map<Hashtag>(hashtagDto);
            return _hashTagRepo.CreateNewHashtag(hashTag);
        }
        catch (Exception e)
        {
            throw new Exception("CreateHashTag in Service went wrong: " + e);
        }
    }

    public async Task<Hashtag> GetHashTagById(int id)
    {
        try
        {
            return await _hashTagRepo.GetHashTagById(id);
        }
        catch (Exception e)
        {
            throw new Exception("GetHashTagById in Service went wrong: " + e);
        }
    }

    public async Task<List<Hashtag>> GetAllHashTags()
    {
        try
        {
            return await _hashTagRepo.GetAllHashTags();
        }
        catch (Exception e)
        {
            throw new Exception("GetAllHashTags in Service went wrong: " + e);
        }
    }

    public Task<Hashtag> DeleteHashtagById(int id)
    {
        try
        {
            return _hashTagRepo.DeleteHashtagById(id);
        }
        catch (Exception e)
        {
            throw new Exception("DeleteHashtagById in Service went wrong: " + e);
        }
    }
    public async Task<Hashtag> FindOrCreateTagAsync(string tag)
    {
        try
        {
            // Attempt to find the hashtag by its tag.
            Hashtag hashtag = await _hashTagRepo.FindByTag(tag);
            
            // If the hashtag doesn't exist, create a new one.
            if (hashtag == null)
            { 
                HashtagDto hashtagDto = new HashtagDto
                {
                    Tag = tag
                };
            
                // Use await to ensure the hashtag is created before proceeding.
                hashtag = await CreateNewHashtag(hashtagDto);
            }


            return hashtag;
        }
        catch (Exception e)
        {
            throw new Exception("FindOrCreateHashtag went wrong: " + e.Message, e);
        }
    }
}
