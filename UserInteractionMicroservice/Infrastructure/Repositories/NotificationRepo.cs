using Entities;
using Infrastructure.Contexts;
using Infrastructure.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            await _dbContext.Notifications.AddAsync(notification);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<List<Notification>> GetNotificationsByUserIdAndType(string creatorId, string type)
    {
        try
        {
            var notifications = _dbContext
                .Notifications.Where(n => n.CreatorId == creatorId && n.Type == type)
                .ToList();

            return Task.FromResult(notifications);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Subscriptions>> GetSubscriptions(string userId)
    {
        try
        {
            var subscriptions = await _dbContext
                .Subscriptions.Where(n => n.FollowerId == userId)
                .ToListAsync();

            return subscriptions;
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

    public async Task<bool> DeleteNotification(int id)
    {
        try
        {
            var notificationToDelete = await _dbContext.Notifications.FirstOrDefaultAsync(n =>
                n.Id == id
            );

            if (notificationToDelete != null)
            {
                _dbContext.Notifications.Remove(notificationToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
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

    public async Task<User> GetUserById(string userId)
    {
        try
        {
            Console.WriteLine($"User ID: {userId}");
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            // If user doesn't exist, create a new one
            if (user == null)
            {
                user = new User
                {
                    Id = userId, /* other properties */
                };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
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

    public async Task<bool> CreateSubscription(Subscriptions subscription)
    {
        try
        {
            Console.WriteLine(subscription.FollowerId);
            Console.WriteLine(subscription.CreatorId);
            Console.WriteLine(subscription.Type);

            await _dbContext.Subscriptions.AddAsync(subscription);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
