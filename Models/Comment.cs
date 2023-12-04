using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace IllinoisProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentDescription { get; set; }
        public DateTime dateTime { get; set; }

        // Foreign keys
        public string AccountId { get; set; }
        public string BlogPostId { get; set; }

        // Navigation properties
        public Account Account { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
