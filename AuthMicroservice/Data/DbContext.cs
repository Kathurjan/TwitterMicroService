using AuthMicroservice.Model;

namespace AuthMicroservice.Data;
using Microsoft.EntityFrameworkCore;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}