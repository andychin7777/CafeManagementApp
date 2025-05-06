using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeManagementApp.SQL.Model
{
    public class Cafe
    {
        /// <summary>
        ///  // UUID for the cafe
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures the database generates the GUID
        public Guid Id { get; set; }

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

        public virtual IList<CafeEmployee> CafeEmployees { get; set; } = new List<CafeEmployee>();
    }
}
