﻿namespace IllinoisProject.Models
{
    public class BlogPost
    {
        public int BlogPostId { get; set; }
        public string BlogName { get; set; }
        public string BlogDescription { get; set; }
        public DateTime PostDate { get; set; }
        public Account Account { get; set; }
    }
}