﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IllinoisProject.Models;
using IllinoisProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Project.Models;

namespace IllinoisProject.Controllers
{
	public class BlogPostController : Controller
	{
		AccountDbContext db;
		private UserManager<ApplicationUser> userManager;
		private SignInManager<ApplicationUser> signInManager;
		private RoleManager<IdentityRole> roleManager;

		public BlogPostController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, AccountDbContext db)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
			this.db = db;
		}

		public async Task<IActionResult> AllBlogPost()
		{
			var blogPost = await db.BlogPosts.Include(c => c.Account).ToListAsync();
			//return View(blogPost);
			// Filter the blog posts to exclude drafts (where Draft is true)
			var publishedBlogPosts = blogPost.Where(blogPost => !blogPost.Draft);

			return View(publishedBlogPosts);
		}
		//For viewing Drafts
		public async Task<IActionResult> AllDraft()
		{
			var blogPost = await db.BlogPosts.Include(c => c.Account).ToListAsync();
			var draftBlogPosts = blogPost.Where(blogPost => blogPost.Draft);

			return View(draftBlogPosts);
		}

		//START OF ADD BLOG POST--------------------------------------------------------------------------------------

		//Loading BlogPost page
		//public async Task<IActionResult> AddBlogPost()
		//{
		//    var user = await userManager.GetUserAsync(User);
		//    var accountDisplay = await db.Accounts.Select(x => new {
		//        Id = x.AccountId, Value = x.AccountName}).ToListAsync();
		//    AccountBlogPostViewModel vm = new AccountBlogPostViewModel();
		//     user.UserName = vm.BlogPost.Account.UserName;
		//    return View(vm);
		//}
		public async Task<IActionResult> AddBlogPost()
		{

			var accountDisplay = await db.Accounts.Select(x => new { Id = x.UserName, Value = x.UserName }).ToListAsync();
			var vm = new AccountBlogPostViewModel
			{
				AccountList = new SelectList(accountDisplay, "Id", "Value")
			};
			return View(vm);
		}

		//Passing the PostBlog and Account class together with data through the viewModel
		[HttpPost]
		public async Task<IActionResult> AddBlogPost(AccountBlogPostViewModel vm)
		{
			var account = await db.Accounts.SingleOrDefaultAsync(i => i.UserName == vm.Account.UserName);
			vm.BlogPost.Account = account;
			db.Add(vm.BlogPost);
			await db.SaveChangesAsync();
			return RedirectToAction("AllBlogPost");
		}
		//END OF ADD BLOG POST

		//Edit Blog Post START------------------------------------------------------------------------------------------
		public async Task<IActionResult> EditBlogPost()
		{
			var accountDisplay = await db.Accounts.Select(x => new { Id = x.AccountId, Value = x.AccountName }).ToListAsync();
			AccountBlogPostViewModel vm = new AccountBlogPostViewModel();
			vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> EditBlogPost(int id, AccountBlogPostViewModel vm)
		{
			var blogPost = await db.BlogPosts.FindAsync(id);

			// update existing blog post with posted data
			blogPost.BlogName = vm.BlogPost.BlogName;
			blogPost.BlogDescription = vm.BlogPost.BlogDescription;

			db.Update(blogPost);
			await db.SaveChangesAsync();

			return RedirectToAction("AllBlogPost");
		}
		//Edit blog post END--------------------------------

		//DELETE BlogPost START ------------------------------------------------------
		public async Task<IActionResult> DeleteBlogPost()
		{
			var accountDisplay = await db.Accounts.Select(x => new {
				Id =
				 x.AccountId,
				Value = x.AccountName
			}).ToListAsync();
			AccountBlogPostViewModel vm = new AccountBlogPostViewModel();
			vm.AccountList = new SelectList(accountDisplay, "Id", "Value");
			return View(vm);
		}
		[HttpPost]
		public async Task<IActionResult> DeleteBlogPost(int id, AccountBlogPostViewModel vm)
		{
			var blogPost = await db.BlogPosts.FindAsync(id);

			if (blogPost == null)
			{
				return NotFound();
			}
			//update existing blog post with posted data
			//blogPost.BlogName = vm.BlogPost.BlogName;
			//blogPost.BlogDescription = vm.BlogPost.BlogDescription;

			db.Remove(blogPost);
			await db.SaveChangesAsync();
			ViewData["BlogName"] = blogPost.BlogName;
			ViewData["BlogDescription"] = blogPost.BlogDescription;


			return RedirectToAction("AllBlogPost");
		}
	}
}
