using Microsoft.EntityFrameworkCore;

namespace dropbox06.Models
{
    public class AccountDbContext:DbContext
    {
        public DbSet<Account> Employees { get; set; }
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) 
        { }
    }
}
