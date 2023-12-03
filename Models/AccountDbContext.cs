using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IllinoisProject.Models;

namespace IllinoisProject.Models
{
    public class AccountDbContext : IdentityDbContext<Account>
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountBlogPost> AccountBlogPosts { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Picture> Pictures { get; set; } 
        public DbSet<Friend> Friends { get; set; }
        public AccountDbContext(DbContextOptions
            <AccountDbContext> options) : base(options) 
        { 
        
        }
    }
}
