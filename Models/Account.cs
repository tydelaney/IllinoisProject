using System.Collections.Generic;
namespace IllinoisProject.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string UserName { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
    
}
