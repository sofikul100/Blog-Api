using FirstApplication.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }




    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
            
        

        modelBuilder.Entity<Category>()
           .HasMany(c => c.Posts)
           .WithOne(p => p.Category)
           .HasForeignKey(p => p.CategoryId);
           


        modelBuilder.Entity<Comment>()
             .HasOne(c => c.User)       
             .WithMany(u => u.Comments)
             .HasForeignKey(c => c.UserId)
              .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);












    }



}