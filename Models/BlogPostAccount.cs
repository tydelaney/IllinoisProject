namespace IllinoisProject.Models
{
    public class BlogPostAccount
    {
        public int BlogPostAccountId { get; set; }

        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public string AccountId { get; set; }
        public Account Account { get; set; }

        public bool IsEditor { get; set; }
    }
}
