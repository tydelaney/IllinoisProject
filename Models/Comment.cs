using Microsoft.AspNetCore.Identity;

namespace IllinoisProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentDescription { get; set; }
        public string UserId { get; set; }
        public DateTime dateTime { get; set; }
        public string BlogPostId { get; set; }

    }
}
