using System.ComponentModel.DataAnnotations;

namespace CafeManagementApp.Server.Model
{
    public class GetCafeViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the cafe
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A short description of the cafe
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Number of the employees.
        /// </summary>
        public long Employees { get; set; }

        /// <summary>
        /// base64 encoded string of the logo image
        /// Logo of the café. This will be used to display a logo image on the front-end.
        /// </summary>        
        public string? Logo { get; set; }

        /// <summary>
        /// Location of the cafe
        /// </summary>
        public string Location { get; set; }
    }
}
