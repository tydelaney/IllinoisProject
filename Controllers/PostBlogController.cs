using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;

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

        public IActionResult AddBlogPost()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBlogPost(BlogPost bp)
        {
            //if (ModelState.IsValid)
            //{
                db.Add(bp); // Add the blogPost to the DbSet
                db.SaveChanges(); // Save changes to the database

                return RedirectToAction("AllBlogPost");
            //}

            return View(bp); // Return to the same view with validation errors
        }

        public IActionResult AllBlogPost()
        {
            return View(db.BlogPosts);
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
