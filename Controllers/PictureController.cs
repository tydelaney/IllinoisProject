using IllinoisProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IllinoisProject.Controllers
{
    public class PictureController : Controller
    {
        private readonly AccountDbContext db;

        public PictureController(AccountDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddPicture()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPicture(Picture p)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", p.MyPicture.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
               await p.MyPicture.CopyToAsync(stream);
            }
            p.Url = p.MyPicture.FileName;
            db.Add(p);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DisplayPictures()
        {
            var pictures = await db.Pictures.ToListAsync();
            return View(pictures);
        }

    }
}
