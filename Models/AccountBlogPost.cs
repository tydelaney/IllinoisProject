namespace IllinoisProject.Models
{
    public class AccountBlogPost
    {
        public string AccountBlogPostId { get; set; }
        public string AccountId { get; set; }
        public string BlogPostId { get; set; }
        public Account Account { get; set; }
        public BlogPost BlogPost { get; set; }
        public string PermissionType { get; set; }
    }
}
