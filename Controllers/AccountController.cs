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
using Microsoft.AspNetCore.Authorization;

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
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                var domain = vm.Email.Split('@').Last();
                if (domain.ToLower() != "illinois.edu")
                {
                    //ModelState.AddModelError("Email", "Only illinois.edu emails are allowed.");
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
            return View("~/Views/BlogPost/AllBlogPost.cshtml");
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
                        return RedirectToAction("AllBlogPost", "BlogPost");
                    }
                }
                ModelState.AddModelError("", "Wrong credentials. Please, try again!");
                return View(vm);
            }
            ModelState.AddModelError("", "Wrong credentials. Please, try again!");
                return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            // Redirect to the home page or another desired page after logout
            return RedirectToAction("Login");
        }
        public IActionResult AllAccount()
        {
            var accounts = db.Accounts
                .Include(a => a.AccountBlogPosts)
                    .ThenInclude(abp => abp.BlogPost) // Include BlogPost related to each AccountBlogPost
                .ToList();

            return View(accounts);
        }

        [Authorize]
        public IActionResult MyAccount()
        {
            // Retrieve the current user's ID
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId == null)
            {
                // User is not logged in, handle accordingly (e.g., redirect to login)
                return RedirectToAction("Login", "Account");
            }

            // Retrieve the current user and their associated blog posts
            var currentAccount = db.Accounts
         .Include(a => a.AccountBlogPosts)
             .ThenInclude(abp => abp.BlogPost) // Include BlogPost related to each AccountBlogPost
         .FirstOrDefault(a => a.Id == currentUserId);

            if (currentAccount == null)
            {
                // Current user not found, handle accordingly (e.g., show an error message)
                return NotFound();
            }

            // Pass the current user to the view
            return View(currentAccount);
        }

        public IActionResult AddAccount() 
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


                return RedirectToAction("MyAccount");
            

            return View(model);
        }


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
        [HttpPost]
        //public async Task<IActionResult> AssignPosts(List<AccountBlogPost> accountBlogPosts)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        foreach (var accountBlogPost in accountBlogPosts)
        //        {
        //            //if (accountBlogPost)
        //            //{
        //            //    db.AccountBlogPosts.Add(accountBlogPost);
        //            //}
        //        }

        //        await db.SaveChangesAsync();

        //        return RedirectToAction("AddFriend", new { id = accountBlogPosts.FirstOrDefault().AccountId });
        //    }

        //    return View(accountBlogPosts);
        //}
        [HttpPost]
        public async Task<IActionResult> AddFriend(string id)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var newFriend = new Friend()
            {
                InviterId = currentUserId,
                InviteeId = id,
                InviteStatus = "Pending"
            };
            TempData["SuccessMessage"] = "Friend successfully added!";
            db.Friends.Add(newFriend);
            await db.SaveChangesAsync();

            return RedirectToAction("AllAccount");
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
