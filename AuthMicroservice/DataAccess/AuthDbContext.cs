using AuthMicroservice.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.DataAccess;
public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options) {
    }
    
    public DbSet<User> UsersTable { get; set; }
}