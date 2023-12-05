using System.ComponentModel.DataAnnotations;

namespace IllinoisProject.Models
{
    public class BlogPost
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Blog Name is required")]
        [MaxLength(200, ErrorMessage = "Blog Name cannot exceed 200 characters")]
        public string BlogName { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(500000, ErrorMessage = "Blog Name cannot exceed 500,0000 characters")]
        public string BlogDescription { get; set; }
        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }
        public ICollection<AccountBlogPost> AccountBlogPosts { get; set; } = new List<AccountBlogPost>();
        [Required(ErrorMessage = "Draft or Publish is required")]
        public bool Draft { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public BlogPost()
        {
            Comments = new List<Comment>();
        }

    }
}
