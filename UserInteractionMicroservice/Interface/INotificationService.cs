
namespace UserInteractionMicroservice.Interface
{
    public interface INotificationService
    {
        public Task<List<string>> GetFollowedEntities(string userId);
         public Task GetNotificationsByUserId(string userId);
    }
}
