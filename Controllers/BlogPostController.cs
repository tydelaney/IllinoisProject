using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            var blogPosts = await db.AccountBlogPosts
                .Include(bp => bp.Account)
                //.Include(bp => bp.AccountBlogPosts) // Include the account related to the blog post
                .Include(bp => bp.BlogPost.Comments) // Include the comments related to the blog post
                .Where(bp => !bp.BlogPost.Draft) // Filter out drafts
                .ToListAsync();

            return View(blogPosts);
        }

        //For viewing Drafts
        [Authorize]
        public async Task<IActionResult> AllDraft()
        {
            var accountBlogPosts = await db.AccountBlogPosts
                .Include(abp => abp.BlogPost)
                .Include(abp => abp.Account)
                .Where(abp => abp.BlogPost.Draft)
                .ToListAsync();

            return View(accountBlogPosts);
        }
        //START OF ADD BLOG POST--------------------------------------------------------------------------------------
        public IActionResult AddBlogPost() => View(new AddBlogPostViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBlogPost(AddBlogPostViewModel vm)
        {

            if (!ModelState.IsValid) return View(vm);

            var user = await userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var post = new BlogPost
            {
                Id = Guid.NewGuid().ToString(),
                BlogName = vm.BlogName,
                BlogDescription = vm.BlogDescription,
                PostDate = DateTime.Now,
                Draft = vm.Draft,
            };
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
            //vm.AccountId = currentUserId;

            // Set the PostDate property to the current date and time
            //vm.PostDate = DateTime.Now;
           // vm.BlogPostId = Guid.NewGuid().ToString();

            var accountBlogPost = new AccountBlogPost
            {
                AccountBlogPostId = Guid.NewGuid().ToString(),
                Account = currentUser,
                BlogPost = post,
                AccountId = currentUser.Id,
                BlogPostId =  post.Id,
                //PermissionType = vm.PermissionType
            };

            // Add the BlogPost to the current user's collection
            currentUser.AccountBlogPosts.Add(accountBlogPost);

            // Add the BlogPost to the database
            db.Add(post);

            // Save changes to the database
            await db.SaveChangesAsync();

            // Check if the blog post is a draft
            if (vm.Draft)
            {
                // If it's a draft, redirect to AllDraft
                return RedirectToAction("AllDraft");
            }
            else
            {
                // If it's not a draft, redirect to MyBlogPost
                return RedirectToAction("MyBlogPost");
            }
        }


        //END OF ADD BLOG POST
        //Edit Blog Post START------------------------------------------------------------------------------------------
        public async Task<IActionResult> EditBlogPost(string id)
        {
            var blogPost = await db.BlogPosts.Include(bp => bp.AccountBlogPosts).FirstOrDefaultAsync(bp => bp.Id == id);

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
        public async Task<IActionResult> EditBlogPost(string id, AccountBlogPostViewModel vm)
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
        public async Task<IActionResult> DeleteBlogPost(string id)
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
        public async Task<IActionResult> DeleteBlogPost(string id, AccountBlogPostViewModel vm)
        {
            var blogPost = await db.BlogPosts.FindAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            var relatedAccountBlogPosts = await db.AccountBlogPosts
                .Where(ab => ab.BlogPostId == id)
                .ToListAsync();
            //new
           
            db.Remove(blogPost);

            await db.SaveChangesAsync();

            ViewData["BlogName"] = blogPost.BlogName;
            ViewData["BlogDescription"] = blogPost.BlogDescription;

            return RedirectToAction("MyBlogPost");
        }

        [Authorize]
        public async Task<IActionResult> MyBlogPost()
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (currentUserId == null)
            {
                // User is not logged in, handle accordingly (e.g., redirect to login)
                return RedirectToAction("Login", "Account");
            }

            var myBlogPosts = await db.AccountBlogPosts
                .Include(abp => abp.BlogPost) //line below abp.AccountId might need to be Account.Id instead idk yet -Ty
                .Where(abp => !abp.BlogPost.Draft && abp.AccountId == currentUserId)
                .ToListAsync();

            return View(myBlogPosts);
        }


        public async Task<IActionResult> PublishDraft(string id)
        {
            var draftBlogPost = await db.BlogPosts.FindAsync(id);

           if (draftBlogPost == null || !draftBlogPost.Draft)
            {
              return NotFound();
            }
            draftBlogPost.Draft = false;

            // Set the PostDate property to the current date and time
            draftBlogPost.PostDate = DateTime.Now;

            // Save changes to the database
            await db.SaveChangesAsync();

            // Redirect to the list of all published blog posts
            return RedirectToAction("MyBlogPost");
        }

        public async Task<IActionResult> ViewBlogPost(string id)
        {
            //var blogPost = await db.AccountBlogPosts
              var blogPost = await db.AccountBlogPosts
        .Include(bp => bp.BlogPost.Comments)
            .ThenInclude(comment => comment.Account)
        .Include(bp => bp.Account) // Include Account of AccountBlogPost
        .FirstOrDefaultAsync(bp => bp.BlogPost.Id == id);

            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }



        // Note from Kai: I have added a lot of comments because I am using chatGPT and I want to make sure everyone can see what the code is doing
        // So there will be a lot of comments for clarification below
        [HttpPost]
        public async Task<IActionResult> AddComment(string blogPostId, string commentDescription)
        {
            // Find the blog post by ID. If not found, return a NotFound result.
            var blogPost = await db.BlogPosts.FindAsync(blogPostId);
            if (blogPost == null)
            {
                return NotFound();
            }

            // Retrieve the current user's ID. If the user is not authenticated, return an Unauthorized result.
            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Create a new Comment object with the provided description, current user's ID, current date and time, and the blog post ID.
            var comment = new Comment
            {
                CommentDescription = commentDescription,
                 AccountId = userId,
                dateTime = DateTime.Now,
                BlogPostId = blogPostId
            };

            // Add the new comment to the database and save the changes.
            db.Add(comment);
            await db.SaveChangesAsync();

            // Redirect the user to the ViewBlogPost view of the blog post they commented on.
            return RedirectToAction("ViewBlogPost", new { id = blogPostId });
        }



    }
}
