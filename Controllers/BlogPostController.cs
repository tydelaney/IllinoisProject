using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Project.Models;

namespace IllinoisProject.Controllers
{
    public class BlogPostController : Controller
    {
        AccountDbContext db;
        private UserManager<ApplicationUser> userManager;
        
        // Replace YourDbContext with your DbContext class

        public BlogPostController(AccountDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> AllBlogPost()
        {
            var blogPost = await db.BlogPosts.Include(c=>c.Account).ToListAsync();
            return View(blogPost);
        }
        

        //START OF ADD BLOG POST--------------------------------------------------------------------------------------

        //Loading BlogPost page
        public async Task<IActionResult> AddBlogPost()
        {
            var accountDisplay = await db.Accounts.Select(x => new {
                Id =
                x.UserName,
                Value = x.UserName
            }).ToListAsync();
            AccountAddAccountViewModel vm = new AccountAddAccountViewModel();
            vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
            return View(vm);
        }

        //Passing the PostBlog and Account class together with data through the viewModel
        [HttpPost]
        public async Task<IActionResult> AddBlogPost(AccountAddAccountViewModel vm)
        {
            var account = await db.Accounts.SingleOrDefaultAsync(i => i.AccountId == vm.Account.AccountId);
            vm.BlogPost.Account = account;
            db.Add(vm.BlogPost);
            await db.SaveChangesAsync();
            return RedirectToAction("AllBlogPost");
        }
        //END OF ADD BLOG POST

        //Edit Blog Post START------------------------------------------------------------------------------------------
        public async Task<IActionResult> EditBlogPost()
        {
            var accountDisplay = await db.Accounts.Select(x => new {
                Id =
                x.AccountId,
                Value = x.AccountName
            }).ToListAsync();
            AccountAddAccountViewModel vm = new AccountAddAccountViewModel();
            vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditBlogPost(int id, AccountAddAccountViewModel vm)
        {
            var blogPost = await db.BlogPosts.FindAsync(id);
           
            // update existing blog post with posted data
            blogPost.BlogName = vm.BlogPost.BlogName;
            blogPost.BlogDescription = vm.BlogPost.BlogDescription;

            db.Update(blogPost);
            await db.SaveChangesAsync();

            return RedirectToAction("AllBlogPost");
        }
        //Edit blog post END--------------------------------

        //DELETE BlogPost START ------------------------------------------------------
        public async Task<IActionResult> DeleteBlogPost()
        {
            var accountDisplay = await db.Accounts.Select(x => new {
                Id =
                 x.AccountId,
                Value = x.AccountName
            }).ToListAsync();
            AccountAddAccountViewModel vm = new AccountAddAccountViewModel();
            vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBlogPost(int id, AccountAddAccountViewModel vm)
        {
            var blogPost = await db.BlogPosts.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }
            //update existing blog post with posted data
            //blogPost.BlogName = vm.BlogPost.BlogName;
            //blogPost.BlogDescription = vm.BlogPost.BlogDescription;

            db.Remove(blogPost);
            await db.SaveChangesAsync();
            ViewData["BlogName"] = blogPost.BlogName;
            ViewData["BlogDescription"] = blogPost.BlogDescription;

           
            return RedirectToAction("AllBlogPost");
        }
    }
}
