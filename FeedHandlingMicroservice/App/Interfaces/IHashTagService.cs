using FeedHandlingMicroservice.Models;

namespace FeedHandlingMicroservice.App;

public interface IHashTagService
{
    Task<Hashtag> CreateNewHashtag(HashtagDto hashtagDto);
    Task<Hashtag> GetHashTagById(int id);
    Task<List<Hashtag>> GetAllHashTags();
    Task<Hashtag> DeleteHashtagById(int id);
}