using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;

namespace Project.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly AccountDbContext _context; // Replace YourDbContext with your DbContext class

        public BlogPostController(AccountDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBlogPost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBlogPost(BlogPost bp)
        {
            if (ModelState.IsValid)
            {
                _context.BlogPosts.Add(bp); // Add the blogPost to the DbSet
                _context.SaveChanges(); // Save changes to the database

                return RedirectToAction("Index");
            }

            return View(bp); // Return to the same view with validation errors
        }

        public IActionResult AllBlogPost()
        {
            return View();
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
