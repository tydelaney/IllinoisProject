using IllinoisProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IllinoisProject.ViewModels
{
    public class AccountBlogPostViewModel
    {
        public Account Account { get; set; }
        public AccountBlogPost AccountBlogPost { get; set; }
        public SelectList AccountList { get; set; }
        public BlogPost BlogPost { get; set; }
     

    }
}
