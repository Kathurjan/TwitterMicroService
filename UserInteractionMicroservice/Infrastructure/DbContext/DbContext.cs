using Entities;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace Infrastructure.Contexts;

public class DbContextManagement : DbContext
{

    public DbContextManagement(DbContextOptions<DbContextManagement> options, IOptions<InfrastructureSettings> infastructureSettings) : base(options)
    {
        
    }
}