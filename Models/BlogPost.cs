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
        public Account Account { get; set; }
        public bool Draft { get; set; }

    }
}
