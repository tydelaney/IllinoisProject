﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.Controllers
{
    public class BlogPostController : Controller
    {
        AccountDbContext db; // Replace YourDbContext with your DbContext class

        public BlogPostController(AccountDbContext db)
        {
            this.db = db;
        }

     //START OF ADD BLOG POST

        //Loading BlogPost page
        public async Task<IActionResult>AddBlogPost()
        {
            var accountDisplay = await db.Accounts.Select(x => new { Id = 
                x.AccountId, Value = x.AccountName }).ToListAsync();
            AccountAddAccountViewModel vm = new AccountAddAccountViewModel();
            vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
            return View(vm);
        }
        
        //Passing the PostBlog and Account class together with data through the viewModel
        [HttpPost]
        public async Task<IActionResult> AddBlogPost(AccountAddAccountViewModel vm)
        {
            var account = await db.Accounts.SingleOrDefaultAsync(i => i.AccountId == vm.Account.AccountId);
            vm.BlogPost.Account = account;
            db.Add(vm.BlogPost);
            await db.SaveChangesAsync();
            return RedirectToAction("AllBlogPost");
        }
     //END OF ADD BLOG POST
        public async Task<IActionResult> AllBlogPost()
        {
            var blogPost = await db.BlogPosts.Include(c=>c.Account).ToListAsync();
            return View(blogPost);
        }
        public IActionResult DeleteBlogPost()
        {
            return View();
        }
        //Edit Blog Post START----------------------------
        public async Task<IActionResult> EditBlogPost()
        {
            var accountDisplay = await db.Accounts.Select(x => new {
                Id =
                x.AccountId,
                Value = x.AccountName
            }).ToListAsync();
            AccountAddAccountViewModel vm = new AccountAddAccountViewModel();
            vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> EditBlogPost(AccountAddAccountViewModel vm)
        {

            var account = await db.Accounts.SingleOrDefaultAsync(i => i.AccountId == vm.Account.AccountId);
            vm.BlogPost.Account = account;

            db.Update(vm.BlogPost);
            await db.SaveChangesAsync();
            return RedirectToAction("AllBlogPost");
        }
        //Edit blog post END--------------------------------
    }
}