using AuthMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.DataAccess;
public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}