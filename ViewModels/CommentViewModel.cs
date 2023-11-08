namespace IllinoisProject.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; } // For editing
        public string CommentDescription { get; set; }
        public int BlogPostId { get; set; } // To know which blog post the comment belongs to
    }

}
