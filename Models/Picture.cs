using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IllinoisProject.Models
{
    public class Picture
    {
        public int PictureId { get; set; }

        public string? AltAttribute { get; set; } // Make it nullable

        [NotMapped]
        public IFormFile MyPicture { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}
