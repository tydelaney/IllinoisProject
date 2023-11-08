using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IllinoisProject.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string AltAttribute { get; set; }

		[NotMapped]
		public IFormFile MyPicture { get; set; }
		[DataType(DataType.Url)]
		public string Url { get; set; }

        // Add a foreign key property to link the Picture to the User
        //public string? UserId { get; set; }

        //// Navigation property to represent the User
        //public ApplicationUser User { get; set; }



    }


}
