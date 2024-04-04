using Entities;
using Infrastructure.Contexts;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Monitor;


namespace Infrastructure.Repositories;

public class NotificationRepo : INotificationRepo
{



    private readonly UserInteractionDbContext _dbContext;
    public NotificationRepo(UserInteractionDbContext dbContext)

    {
        _dbContext = dbContext;
    }

    public async Task CreateNotification(Notification notification)
    {
        try
        {
            _dbContext.Notifications.Add(notification);

            _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Notification> GetNotificationById(int id)
    {
        try
        {
            return _dbContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);
        }
        catch (Exception e)
        {
            MonitorService.Log.Error(e.Message);
            throw new Exception("Did not find notification with that id");
        }
    }

    public Task<List<Notification>> GetNotificationsByUserId(string userId)
    {
        try
        {
            return _dbContext.Notifications.Where(n => n.UserId == userId).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<Notification> UpdateNotification(Notification notification)
    {
        try
        {
            _dbContext.Notifications.Update(notification);
            _dbContext.SaveChangesAsync();
            return Task.FromResult(notification);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<bool> DeleteNotification(int id)
    {
        try
        {
            var notificationToDelete = _dbContext.Notifications.FirstOrDefault(n => n.Id == id);

            if (notificationToDelete != null)
            {
                _dbContext.Notifications.Remove(notificationToDelete);
                _dbContext.SaveChangesAsync();
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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
