using dropbox06.Models;
using dropbox06.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography.Xml;

namespace dropbox06.Controllers
{
    public class AccountController : Controller
    {

        //field
        AccountDbContext db;
        public AccountController(AccountDbContext db)
        {
                this.db = db;
        }

        public IActionResult AllEmployee()
        {
            return View(db.Employees);
        }

        public  IActionResult AddEmployee() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Account employee) 
        {
            db.Add(employee);
            db.SaveChanges();
            
            return RedirectToAction("AllEmployee");
        }
       
        public IActionResult EditEmployee(int id)
        {
            Account employee;
            employee = db.Employees.Find(id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult EditEmployee(Account employee)
        {
            db.Update(employee);
            db.SaveChanges();
            return RedirectToAction("AllEmployee");
        }
        public IActionResult DeleteEmployee(int id)
        {
            Account employee;
            employee = db.Employees.Find(id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult DeleteEmployee(Account employee)
        {
            db.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("AllEmployee");
        }
    }
}
