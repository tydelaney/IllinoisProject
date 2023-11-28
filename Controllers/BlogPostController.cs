using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IllinoisProject.Controllers
{
    public class BlogPostController : Controller
    {
        AccountDbContext db;
        private UserManager<Account> userManager;
        private SignInManager<Account> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public BlogPostController(UserManager<Account> userManager, SignInManager<Account> signInManager, RoleManager<IdentityRole> roleManager, AccountDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.db = db;
        }

        public async Task<IActionResult> AllBlogPost()
        {
            var blogPost = await db.BlogPosts.Include(c => c.Account).ToListAsync();
            var publishedBlogPosts = blogPost.Where(blogPost => !blogPost.Draft);
            return View(publishedBlogPosts);
        }
        //For viewing Drafts
        public async Task<IActionResult> AllDraft()
        {
            var blogPost = await db.BlogPosts.Include(c => c.Account).ToListAsync();
            var draftBlogPosts = blogPost.Where(blogPost => blogPost.Draft);
            return View(draftBlogPosts);
        }
        //START OF ADD BLOG POST--------------------------------------------------------------------------------------
        public async Task<IActionResult> AddBlogPost()
        {

            var accountDisplay = await db.Users.Select(x => new { Id = x.UserName, Value = x.UserName }).ToListAsync();
            var vm = new AccountBlogPostViewModel
            {
                AccountList = new SelectList(accountDisplay, "Id", "Value")
            };
            return View(vm);
        }
        //Passing the PostBlog and Account class together with data through the viewModel
        //[HttpPost]
        //public async Task<IActionResult> AddBlogPost(AccountBlogPostViewModel vm)
        //{
        //    var account = await db.Accounts.FirstOrDefaultAsync(i => i.UserName == vm.Account.UserName);
        //    vm.BlogPost.Account = account;
        //    account.BlogPosts.Add(vm.BlogPost); // Add the BlogPost to the collection
        //    db.Add(vm.BlogPost);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("AllBlogPost");
        //}
        [HttpPost]
        public async Task<IActionResult> AddBlogPost(AccountBlogPostViewModel vm)
        {
            // Retrieve the current user's ID
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
            {
                // User is not logged in, handle accordingly (e.g., redirect to login)
                return RedirectToAction("Login", "Account");
            }

            // Retrieve the current user
            var currentUser = await db.Accounts.FirstOrDefaultAsync(i => i.Id == currentUserId);

            if (currentUser == null)
            {
                // Current user not found, handle accordingly (e.g., show an error message)
                return NotFound();
            }

            // Set the BlogPost's Account property to the current user
            vm.BlogPost.Account = currentUser;

            // Set the PostDate property to the current date and time
            vm.BlogPost.PostDate = DateTime.Now;

            // Add the BlogPost to the current user's collection
            currentUser.BlogPosts.Add(vm.BlogPost);

            // Add the BlogPost to the database
            db.Add(vm.BlogPost);

            // Save changes to the database
            await db.SaveChangesAsync();

            return RedirectToAction("MyBlogPost");
        }


        //END OF ADD BLOG POST
        //Edit Blog Post START------------------------------------------------------------------------------------------
        public async Task<IActionResult> EditBlogPost(int id)
        {
            var blogPost = await db.BlogPosts.Include(bp => bp.Account).FirstOrDefaultAsync(bp => bp.BlogPostId == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            var accountDisplay = await db.Accounts.Select(x => new { Id = x.Id, Value = x.UserName }).ToListAsync();
            var vm = new AccountBlogPostViewModel
            {
                BlogPost = blogPost,
                AccountList = new SelectList(accountDisplay, "Id", "Value")
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditBlogPost(int id, AccountBlogPostViewModel vm)
        {
            var blogPost = await db.BlogPosts.FindAsync(id);
            // update existing blog post with posted data
            blogPost.BlogName = vm.BlogPost.BlogName;
            blogPost.BlogDescription = vm.BlogPost.BlogDescription;

            db.Update(blogPost);
            await db.SaveChangesAsync();

            return RedirectToAction("MyBlogPost");
        }
        //Edit blog post END--------------------------------

        //DELETE BlogPost START ------------------------------------------------------
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            // Assuming you have logic to retrieve the blog post from the database
            var blogPost = await db.BlogPosts.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            var viewModel = new AccountBlogPostViewModel
            {
                BlogPost = blogPost
                // Add any other properties you need to initialize in the view model
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBlogPost(int id, AccountBlogPostViewModel vm)
        {
            var blogPost = await db.BlogPosts.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            db.Remove(blogPost);
            await db.SaveChangesAsync();
            ViewData["BlogName"] = blogPost.BlogName;
            ViewData["BlogDescription"] = blogPost.BlogDescription;
            return RedirectToAction("MyBlogPost");
        }

        public async Task<IActionResult> MyBlogPost()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (currentUserId == null)
            {
                // User is not logged in, handle accordingly (e.g., redirect to login)
                return RedirectToAction("Login", "Account");
            }

            var myBlogPosts = await db.BlogPosts
                .Include(bp => bp.Account)
                .Where(bp => !bp.Draft && bp.Account.Id == currentUserId)
                .ToListAsync();

            return View(myBlogPosts);
        }

        public async Task<IActionResult> PublishDraft(int id)
        {
            var draftBlogPost = await db.BlogPosts.FindAsync(id);

            if (draftBlogPost == null || !draftBlogPost.Draft)
            {
                return NotFound();
            }

            // Update the draft status to publish
            draftBlogPost.Draft = false;

            // Set the PostDate property to the current date and time
            draftBlogPost.PostDate = DateTime.Now;

            // Save changes to the database
            await db.SaveChangesAsync();

            // Redirect to the list of all published blog posts
            return RedirectToAction("MyBlogPost");
        }



    }
}
