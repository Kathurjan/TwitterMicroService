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

    public Task<Notification> CreateNotification(Notification notification)
    {
        throw new NotImplementedException();
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
}
