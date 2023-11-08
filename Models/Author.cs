namespace IllinoisProject.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public Account Account { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
