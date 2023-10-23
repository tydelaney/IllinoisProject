using Microsoft.EntityFrameworkCore;

namespace IllinoisProject.Models
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public AccountDbContext(DbContextOptions
            <AccountDbContext> options) : base(options) 
        { 
        
        }
    }
}
