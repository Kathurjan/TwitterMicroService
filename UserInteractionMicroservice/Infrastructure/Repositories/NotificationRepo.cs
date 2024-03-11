using Entities;
using Infrastructure.IRepositories;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;


public class NotificationRepo : INotificationRepo
{
    private readonly DbContextManagement _context;
    public NotificationRepo(DbContextManagement context)
    {
        _context = context;
    }

    public async Task<Notification> CreateNotification(Notification notification)
    {
        Console.WriteLine("Creating notification");
        Console.WriteLine(notification.UserId);
        Console.WriteLine(notification.Type);
        Console.WriteLine(notification.Message);

        _context.Notifications.Add(notification);

        await _context.SaveChangesAsync();

        return notification;
    }

    Task<Notification> INotificationRepo.GetNotificationById(int id)
    {
        throw new NotImplementedException();
    }

    Task<List<Notification>> INotificationRepo.GetNotificationsByUserId(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<Notification> UpdateNotification(Notification notification)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteNotification(int id)
    {
        throw new NotImplementedException();
    }

    public async Task CreateTestUser(User user)
    {
        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }
}
