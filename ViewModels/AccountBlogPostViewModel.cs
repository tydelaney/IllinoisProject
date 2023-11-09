using IllinoisProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IllinoisProject.ViewModels
{
    public class AccountBlogPostViewModel
    {
        public Account Account { get; set; }
        public SelectList AccountList { get; set; }
        public BlogPost BlogPost { get; set; }
     

    }
}
