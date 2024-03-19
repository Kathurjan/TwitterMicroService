using Entities;
using Infrastructure.IRepositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Repositories;


public class NotificationRepo : INotificationRepo
{
    private readonly DbContextManagement _context;
    public NotificationRepo(DbContextManagement context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));

    }

    public Notification CreateNotification(Notification notification)
    {
        Console.WriteLine("Creating notification");
        Console.WriteLine(notification.UserId);
        Console.WriteLine(notification.Type);
        Console.WriteLine(notification.Message);

        _context.Notifications.Add(notification);

        _context.SaveChangesAsync();

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

    public void RebuildDB()
    {
        try
        {
            string connectionString = _context.Database.GetDbConnection().ConnectionString;
            Console.WriteLine($"Connection String: {connectionString}");
            _context.Database.EnsureCreatedAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
    }
}
