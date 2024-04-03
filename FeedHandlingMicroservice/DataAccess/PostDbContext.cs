using FeedHandlingMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedHandlingMicroservice.DataAccess;
public class PostDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public PostDbContext(DbContextOptions<PostDbContext> options)
        : base(options) {
    }
    public DbSet<Post> PostsTable { get; set; }
    public DbSet<Hashtag> Hashtags { get; set; }
    public DbSet<PostHashtag> PostHashtags { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Establishing composite primary key for PostHashtag entity.
        // This ensures that each combination of PostId and HashtagId is unique.
        modelBuilder.Entity<PostHashtag>()
            .HasKey(ph => new { ph.PostId, ph.HashtagId });
    
        // Configuring the one-to-many relationship from PostHashtag to Post.
        // establishes the foreign key constraint in the PostHashtag table pointing to the Post table.
        modelBuilder.Entity<PostHashtag>()
            .HasOne(ph => ph.Post)
            .WithMany(p => p.Hashtags)
            .HasForeignKey(ph => ph.PostId);
        // Configuring the one-to-many relationship from PostHashtag to Hashtag.
        // This defines the foreign key constraint in the PostHashtag table pointing to the Hashtag table.
        modelBuilder.Entity<PostHashtag>()
            .HasOne(ph => ph.Hashtag)
            .WithMany(h => h.Posts)
            .HasForeignKey(ph => ph.HashtagId);
    }

}