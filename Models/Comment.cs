namespace IllinoisProject.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentDescription { get; set; }
        public Account account { get; set; }
        public DateTime dateTime { get; set; }
        public BlogPost? BlogPost { get; set; }
        public int BlogPostId { get; set; }

    }
}
