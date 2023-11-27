using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IllinoisProject.Models;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Azure.Identity;

namespace IllinoisProject.Controllers
{
    public class AccountController : Controller
    {

        private AccountDbContext db;
        private UserManager<Account> userManager;
        private SignInManager<Account> signInManager;
        private RoleManager<IdentityRole> roleManager;

        // Constructor 
        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager, RoleManager<IdentityRole> roleManager, AccountDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.db = db;
        }

        //public IActionResult Register()
        //{
        //    return View();
        //}
        public IActionResult Register() => View(new AccountRegisterViewModel());

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var domain = vm.Email.Split('@').Last();
                if (domain.ToLower() != "illinois.edu")
                {
                    ModelState.AddModelError("Email", "Only illinois.edu emails are allowed.");
                    return View(vm);
                }

                var user = await userManager.FindByEmailAsync(vm.Email);
                if (user != null)
                {
                    ModelState.AddModelError("Email","This email address is already in use");
                    return View(vm);
                }

                var newUser = new Account()
                {
                    UserName = vm.UserName, Email = vm.Email, Name = vm.Name 
                };
                var result = await userManager.CreateAsync(newUser, vm.Password);
                if (result.Succeeded)
                {
                    await db.SaveChangesAsync();
                    await signInManager.SignInAsync(newUser, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("AllAccount");
        }

        public IActionResult Login() => View(new AccountLoginViewModel());

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var user = await userManager.FindByEmailAsync(vm.Email);
            if (user != null)
            {
                var passwordCheck = await userManager.CheckPasswordAsync(user, vm.Password);
                if (passwordCheck)
                {
                    var result = await signInManager.PasswordSignInAsync(user, vm.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("AddBlogPost", "BlogPost");
                    }
                }
                ModelState.AddModelError("", "Wrong credentials. Please, try again!");
                return View(vm);
            }
            ModelState.AddModelError("", "Wrong credentials. Please, try again!");
                return View(vm);
        }

        public IActionResult AllAccount()
        {
            var accounts = db.Accounts.Include(a => a.BlogPosts).ToList();
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

        public IActionResult EditAccount(string id)
        {
            var user = db.Users.Include(u => u.Picture).FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount(Account model, IFormFile profilePicture)
        {
           
                var user = await userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    return NotFound();
                }

                user.Name = model.Name;
                user.UserName = model.UserName;
                user.Email = model.Email;

                // Update the profile picture if a new one is provided
                if (profilePicture != null)
                {
                    // Save the new profile picture
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", profilePicture.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await profilePicture.CopyToAsync(stream);
                    }

                    // Update the user's profile picture URL
                    user.Picture = new Picture { Url = profilePicture.FileName };
                }

                await userManager.UpdateAsync(user);

                return RedirectToAction("AllAccount");
            

            return View(model);
        }


        //public  async Task <IActionResult> EditAccount(string id)
        //{
        //    Account account;
        //    account = db.Accounts.Find(id);
        //    return View(account);
        //}




        //[HttpPost]
        //public async Task <IActionResult> EditAccount(Account account)
        //{

        //    db.Update(account);
        //    db.SaveChangesAsync();
        //    return RedirectToAction("AllAccount");
        //}
        //edit account end
        public IActionResult DeleteAccount(string id)
        {

            var user = db.Users.Include(u => u.Picture).FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);

        }
        [HttpPost]
        public async Task <IActionResult> DeleteAccount(Account model, IFormFile profilePicture)
        {

            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = model.Name;
            user.UserName = model.UserName;
            user.Email = model.Email;

            // Update the profile picture if a new one is provided
            if (profilePicture != null)
            {
                // Save the new profile picture
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", profilePicture.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }

                // Update the user's profile picture URL
                user.Picture = new Picture { Url = profilePicture.FileName };
            }
            await userManager.DeleteAsync(user);


            return RedirectToAction("AllAccount");


            return View(model);
            //db.Remove(account);
            //db.SaveChanges();
            //return RedirectToAction("AllAccount");
        }

        public  async Task<IActionResult> AddFriend(int id)
        {
            var account = db.Accounts.Find(id);
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var myAccount = await db.Accounts.FirstOrDefaultAsync(i => i.Id == currentUserId);
            var myBlogPosts = myAccount.BlogPosts;

            //var blogposts = account.BlogPosts;
            return View(myBlogPosts);
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
            return RedirectToAction("AllAccount");
        }

        public async Task<IActionResult> DisplayPictures()
        {
            var pictures = await db.Pictures.ToListAsync();
            return View(pictures);
        }
    }
}
