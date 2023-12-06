using IllinoisProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IllinoisProject.ViewModels
{
    public class AddBlogPostViewModel
    {
        [Required]
        public string BlogName { get; set; }

        [Required]
        public string BlogDescription { get; set; }
        [Required]
        public bool Draft { get; set; }
        public string? PermissionType { get; set; }
    }
}
