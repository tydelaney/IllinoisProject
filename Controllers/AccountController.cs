﻿
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;

namespace IllinoisProject.Controllers
{
    public class AccountController : Controller
    {

        private AccountDbContext db;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private RoleManager<IdentityRole> roleManager;

        // Constructor 
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, AccountDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.db = db;
        }

        ////field
        //AccountDbContext db;
        //public AccountController(AccountDbContext db)
        //{
        //        this.db = db;
        //}
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                var domain = model.Email.Split('@').Last();
                if (domain.ToLower() != "illinois.edu")
                {
                    ModelState.AddModelError("Email", "Only illinois.edu emails are allowed.");
                    return View(model);
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);
                 
                if (result.Succeeded)
                {
                    return RedirectToAction("AllBlogPost", "BlogPost");
                }

                ModelState.AddModelError("", "Login Failure.");
            }
            return View(vm);
        }

        public IActionResult AllAccount()
        {
            return View(db.Accounts);
        }

   
       //edit account start
        public IActionResult EditAccount(int id)
        {
            Account account;
            account = db.Accounts.Find(id);
            return View(account);
        }
        [HttpPost]
        public IActionResult EditAccount(Account account)
        {
          
            db.Update(account);
            db.SaveChanges();
            return RedirectToAction("AllAccount");
        }
        //edit account end
        public IActionResult DeleteAccount(int id)
        {
            //Account account;
            //account = db.Accounts.Find(id);
            var account = db.Accounts.Find(id);
            return View(account);
        }
        [HttpPost]
        public IActionResult DeleteAccount(Account account)
        {
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("AllAccount");
        }

        /// picture 
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
            return RedirectToAction("AllAccount");
        }

        public async Task<IActionResult> DisplayPictures()
        {
            var pictures = await db.Pictures.ToListAsync();
            return View(pictures);
        }

    }
}

