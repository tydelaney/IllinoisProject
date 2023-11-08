using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IllinoisProject.Models;

namespace IllinoisProject.Models
{
    public class AccountDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<Picture>Pictures { get; set; } 
        public AccountDbContext(DbContextOptions
            <AccountDbContext> options) : base(options) 
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define one-to-many relationship between BlogPost and Comments
            modelBuilder.Entity<BlogPost>()
                .HasMany(b => b.Comments)
                .WithOne(c => c.BlogPost) // Assuming Comment has a BlogPost navigation property
                .HasForeignKey(c => c.BlogPostId);

            // Define one-to-many relationship between Account and Comments
            modelBuilder.Entity<Comment>()
                .HasOne(p => p.BlogPost)
                .WithMany(b => b.Comments)
                .HasForeignKey(p => p.BlogPostId)
                .OnDelete(DeleteBehavior.Restrict); // or .OnDelete(DeleteBehavior.ClientSetNull)

            // Additional model configuration goes here
        }

    }
}
