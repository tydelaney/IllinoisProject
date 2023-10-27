using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IllinoisProject.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        [Required]
        public string AltAttribute { get; set; }
        [NotMapped]
        public IFormFile MyPicture { get; set; }
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}
