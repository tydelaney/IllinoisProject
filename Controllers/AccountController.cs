using IllinoisProject.ViewModels;
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IllinoisProject.Models;
using System.Security.Cryptography.Xml;

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

        public  IActionResult AddAccount() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAccount(Account account) 
        {
            db.Add(account);
            await db.SaveChangesAsync();
            
            return RedirectToAction("AllAccount");
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
            Account account;
            account = db.Accounts.Find(id);
            return View(account);
        }
        [HttpPost]
        public IActionResult DeleteAccount(Account account)
        {
            db.Remove(account);
            db.SaveChanges();
            return RedirectToAction("AllAccount");
        }
    }
}
