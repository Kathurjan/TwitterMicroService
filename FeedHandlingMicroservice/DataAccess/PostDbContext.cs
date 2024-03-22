﻿using FeedHandlingMicroservice.Models;
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

        modelBuilder.Entity<PostHashtag>()
            .HasKey(ph => new { ph.PostId, ph.HashtagId });

        modelBuilder.Entity<PostHashtag>()
            .HasOne(ph => ph.Post)
            .WithMany(p => p.Hashtags)
            .HasForeignKey(ph => ph.PostId);

        modelBuilder.Entity<PostHashtag>()
            .HasOne(ph => ph.Hashtag)
            .WithMany(h => h.Posts)
            .HasForeignKey(ph => ph.HashtagId);
    }

}