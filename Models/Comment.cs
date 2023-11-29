using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace IllinoisProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentDescription { get; set; }
        public string UserId { get; set; }
        public DateTime dateTime { get; set; }
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
        public Account User { get; set; }
    }
}
