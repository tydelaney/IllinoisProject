using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;

namespace IllinoisProject.Controllers
{
    public class AccountController : Controller
    {

        //field
        AccountDbContext db;
        public AccountController(AccountDbContext db)
        {
                this.db = db;
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
        public IActionResult AddAccount(Account account) 
        {
            db.Add(account);
            db.SaveChanges();
            
            return RedirectToAction("AllAccount");
        }
       
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
