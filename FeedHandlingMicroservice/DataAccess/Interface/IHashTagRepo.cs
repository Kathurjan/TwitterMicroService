using FeedHandlingMicroservice.Models;

namespace FeedHandlingMicroservice.DataAccess;

public interface IHashTagRepo
{
    Task<Hashtag> CreateNewHashtag(Hashtag hashtag);
    Task<Hashtag> GetHashTagById(int id);
    Task<List<Hashtag>> GetAllHashTags();
    Task<Hashtag> DeleteHashtagById(int id);
    Task<Hashtag> FindByTag(string tag);
}