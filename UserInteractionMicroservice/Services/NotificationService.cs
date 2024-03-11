using UserInteractionMicroservice.Interface;
namespace UserInteractionMicroservice.Services
{
    public class NotificationService : INotificationService
    {
        public Task<List<string>> GetFollowedEntities(string userId)
        {
            List<string> followedEntities = new List<string>()
            {
                "abcd1", // Example entity ID
                "abcd2", // Example entity ID
                "abcd3"  // Example entity ID
            };

            return Task.FromResult(followedEntities);
        }

        public Task GetNotificationsByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
