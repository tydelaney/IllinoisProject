using IllinoisProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;

namespace IllinoisProject.ViewModels
{
    public class AccountAddAccountViewModel
    {
        public Account Account { get; set; }
        public SelectList AccountList { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public SelectList ApplicationUserList { get; set; } 
        public BlogPost BlogPost { get; set; }
     

    }
}
