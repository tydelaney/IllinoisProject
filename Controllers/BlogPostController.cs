using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.Controllers
{
    public class BlogPostController : Controller
    {
        AccountDbContext db; // Replace YourDbContext with your DbContext class

        public BlogPostController(AccountDbContext db)
        {
            this.db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult>AddBlogPost()
        {
            var accountDisplay = await db.Accounts.Select(x => new { Id = 
                x.AccountId, Value = x.AccountName }).ToListAsync();
            AccountAddAccountViewModel vm = new AccountAddAccountViewModel();
            vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
            return View(vm);
        }
        //[HttpPost]
        //public IActionResult AddBlogPost(BlogPost bp)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //        db.Add(bp); // Add the blogPost to the DbSet
        //        db.SaveChanges(); // Save changes to the database

        //        return RedirectToAction("AllBlogPost");
        //    //}

        //    return View(bp); // Return to the same view with validation errors
        //}
        [HttpPost]
        public async Task<IActionResult> AddBlogPost(AccountAddAccountViewModel vm)
        {
            var account = await db.Accounts.SingleOrDefaultAsync(i => i.AccountId == vm.Account.AccountId);
            vm.BlogPost.Account = account;
            db.Add(vm.BlogPost);
            await db.SaveChangesAsync();
            return RedirectToAction("AllBlogPost");
        }
        public async Task<IActionResult> AllBlogPost()
        {
            var blogPost = await db.BlogPosts.Include(c=>c.Account).ToListAsync();
            return View(blogPost);
        }
        public IActionResult DeleteBlogPost()
        {
            return View();
        }

        public IActionResult EditBlogPost()
        {
            return View();
        }
    }
}
