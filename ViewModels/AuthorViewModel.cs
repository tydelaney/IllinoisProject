using IllinoisProject.Models;

namespace IllinoisProject.ViewModels
{
    public class AuthorViewModel
    {
        public BlogPostAccount author { get; set; }
        public Account Account { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
