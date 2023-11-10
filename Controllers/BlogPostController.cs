using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
namespace IllinoisProject.Controllers
{
    public class BlogPostController : Controller
    {
        AccountDbContext db;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        public BlogPostController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, AccountDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.db = db;
        }

        public async Task<IActionResult> AllBlogPost()
        {
            var blogPosts = await db.BlogPosts.Include(c => c.Account)
                                              .Include(c => c.Comments)
                                               .ThenInclude(c => c.account) // Make sure to load the account for each comment
                                               .Where(bp => !bp.Draft)
                                               .ToListAsync();


            return View(blogPosts);
        }

        //For viewing Drafts
        public async Task<IActionResult> AllDraft()
        {
            var blogPost = await db.BlogPosts.Include(c => c.Account).ToListAsync();
            var draftBlogPosts = blogPost.Where(blogPost => blogPost.Draft);

            return View(draftBlogPosts);
        }

        //START OF ADD BLOG POST--------------------------------------------------------------------------------------

        //Loading BlogPost page
        //public async Task<IActionResult> AddBlogPost()
        //{
        //    var user = await userManager.GetUserAsync(User);
        //    var accountDisplay = await db.Accounts.Select(x => new {
        //        Id = x.AccountId, Value = x.AccountName}).ToListAsync();
        //    AccountBlogPostViewModel vm = new AccountBlogPostViewModel();
        //     user.UserName = vm.BlogPost.Account.UserName;
        //    return View(vm);
        //}
        public async Task<IActionResult> AddBlogPost()
        {

            var accountDisplay = await db.Accounts.Select(x => new{Id = x.UserName,Value = x.UserName}).ToListAsync();
            var vm = new AccountBlogPostViewModel
            {
                AccountList = new SelectList(accountDisplay, "Id", "Value")
            };
            return View(vm);
        }

        //Passing the PostBlog and Account class together with data through the viewModel
        [HttpPost]
        public async Task<IActionResult> AddBlogPost(AccountBlogPostViewModel vm)
        {
            var account = await db.Accounts.FirstOrDefaultAsync(i => i.UserName == vm.Account.UserName);
            vm.BlogPost.Account = account;
            account.BlogPosts.Add(vm.BlogPost); // Add the BlogPost to the collection
            db.Add(vm.BlogPost);
            await db.SaveChangesAsync();
            return RedirectToAction("AllBlogPost");
        }
        //END OF ADD BLOG POST

        //Edit Blog Post START------------------------------------------------------------------------------------------
        public async Task<IActionResult> EditBlogPost()
        {
            var accountDisplay = await db.Accounts.Select(x => new {Id = x.AccountId,Value = x.AccountName}).ToListAsync();
            AccountBlogPostViewModel vm = new AccountBlogPostViewModel();
            vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
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
            AccountBlogPostViewModel vm = new AccountBlogPostViewModel();
            vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBlogPost(int id, AccountBlogPostViewModel vm)
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

        // Method to show the form for adding a new comment to a blog post
        [HttpGet]
        public IActionResult AddComment(int blogPostId)
        {
            var viewModel = new CommentViewModel { BlogPostId = blogPostId };
            return RedirectToAction("AllBlogPost");
        }

        // Method to handle the post request for adding a new comment
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user1 = await userManager.GetUserAsync(User);
                Account account = db.Accounts.FirstOrDefault(a => a.UserId == user1.Id);
                var user = await userManager.GetUserAsync(User);
                var comment = new Comment
                {
                    CommentDescription = viewModel.CommentDescription,
                    dateTime = DateTime.Now,
                    account = account,
                    BlogPostId = viewModel.BlogPostId
                };
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction("AllBlogPost");
            }
            return RedirectToAction("AllBlogPost");
        }

        // Method to show the form for editing an existing comment
        [HttpGet]
        public async Task<IActionResult> EditComment(int id)
        {
            var comment = await db.Comments
                .Include(c => c.account)
                .FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null || comment.account.AccountEmail != User.Identity.Name)
            {
                return NotFound();
            }

            var viewModel = new CommentViewModel
            {
                CommentId = comment.CommentId,
                CommentDescription = comment.CommentDescription,
                BlogPostId = comment.BlogPostId // Make sure this is the property name in your BlogPost entity
            };
            return View(viewModel); // Return the view with the view model
        }


        // POST: BlogPost/EditComment
        [HttpPost]
        public async Task<IActionResult> EditComment(int id, string commentDescription)
        {
            var comment = await db.Comments.Include(c => c.account).FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null || (comment.account.AccountEmail != User.Identity.Name && !User.IsInRole("Admin")))
            {
                return NotFound();
            }

            comment.CommentDescription = commentDescription;
            await db.SaveChangesAsync();

            return RedirectToAction("AllBlogPost");
        }

        // POST: BlogPost/DeleteComment
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await db.Comments
                                  .Include(c => c.account) // Make sure to include the account object
                                  .FirstOrDefaultAsync(c => c.CommentId == id);

            // Check if the comment or the associated account is null
            if (comment == null || comment.account == null)
            {
                return NotFound();
            }

            // Check if the user is the one who made the comment or if they are an admin
            if (comment.account.AccountEmail != User.Identity.Name && !User.IsInRole("Admin"))
            {
                // Optionally, return some form of "Unauthorized" response here instead of NotFound
                return NotFound();
            }

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            return RedirectToAction("AllBlogPost");
        }



    }
}
