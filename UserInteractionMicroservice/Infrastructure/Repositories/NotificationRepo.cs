using Entities;
using Sockets;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DbContext = Infrastructure.Contexts.DbContext;


namespace Infrastructure.Repositories;


public class NotificationRepo : INotificationRepo
{
    private readonly DbContext _dbContext;
    public NotificationRepo(DbContext dbContext)
    {
        _dbContext = dbContext;

    }

    public async Task CreateNotification(Notification notification)
    {
        Console.WriteLine("Creating notification");
        Console.WriteLine(notification.UserId);
        Console.WriteLine(notification.Type);
        Console.WriteLine(notification.Message);

        _dbContext.Notifications.Add(notification);

        await _dbContext.SaveChangesAsync();
        
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

    public async Task<ActionResult<string>> CreateTestUser(User user)
    {
        _dbContext.Users.Add(user);

        await _dbContext.SaveChangesAsync();

        return "User created";
    }

    public void RebuildDB()
    {
        try
        {
            string connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
            Console.WriteLine($"Connection String: {connectionString}");
            _dbContext.Database.EnsureCreatedAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
        }
    }
}
