using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IllinoisProject.ViewModels
{
    public class AddPictureViewModel
    {
        [Required(ErrorMessage = "Alt attribute is required.")]
        [Display(Name = "Alt Attribute")]
        public string AltAttribute { get; set; }

        [Display(Name = "Select a file to upload")]
        public IFormFile MyPicture { get; set; }
    }
}
