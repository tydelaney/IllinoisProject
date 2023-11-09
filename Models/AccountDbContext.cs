using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Project.Models;

namespace IllinoisProject.Models
{
	public class AccountDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Account> Accounts { get; set; }
		public DbSet<BlogPost> BlogPosts { get; set; }

		public DbSet<Picture> Pictures { get; set; }
		public AccountDbContext(DbContextOptions
			<AccountDbContext> options) : base(options)
		{

		}


	

	}
}

