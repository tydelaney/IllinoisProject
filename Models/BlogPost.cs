using System.ComponentModel.DataAnnotations;

namespace IllinoisProject.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string BlogName { get; set; }
        public string BlogDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }
        public ICollection<AccountBlogPost> AccountBlogPosts { get; set; } = new List<AccountBlogPost>();
        public bool Draft { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public BlogPost()
        {
            Comments = new List<Comment>();
        }

    }
}
