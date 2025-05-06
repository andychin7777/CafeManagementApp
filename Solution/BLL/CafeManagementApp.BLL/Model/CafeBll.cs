using System.ComponentModel.DataAnnotations;

namespace CafeManagementApp.BLL.Model
{
    public class CafeBll
    {
        /// <summary>
        ///  // UUID for the cafe
        /// </summary>
        public Guid CafeGuid { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        /// <summary>
        /// base64 encoded string of the logo image
        /// </summary>        
        public string? Logo { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string Location { get; set; }

        public virtual IList<CafeEmployeeBll> CafeEmployees { get; set; } = new List<CafeEmployeeBll>();
    }
}
