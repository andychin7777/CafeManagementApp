using System.ComponentModel.DataAnnotations;

namespace CafeManagementApp.Server.Model
{
    public class CafeViewModel
    {
        /// <summary>
        ///  // UUID for the cafe
        /// </summary>
        public Guid CafeGuid { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string? Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// base64 encoded string of the logo image
        /// </summary>
        /// 
        [DataType(DataType.Html)]
        public string? Logo { get; set; }


        [Required]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string? Location { get; set; }
    }
}
