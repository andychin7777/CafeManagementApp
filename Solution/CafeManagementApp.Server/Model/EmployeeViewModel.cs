using CafeManagementApp.BLL.Model;
using System.ComponentModel.DataAnnotations;

namespace CafeManagementApp.Server.Model
{
    public class EmployeeViewModel
    {
        public long EmployeeId { get; set; }

        [Required]
        [RegularExpression(@"^UI[A-Za-z0-9]{7}$",
            ErrorMessage = "ID must be in the format 'UIXXXXXXX' where X is alphanumeric.")]
        public string? EmployeeIdString { get; set; }

        [Required]
        [StringLength(500)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^[89]\d{7}$", ErrorMessage = "Phone number must start with 8 or 9 and have 8 digits.")]
        public string? PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Gender must be either 'Male' or 'Female'.")]
        public string? Gender { get; set; }

        public virtual IList<CafeEmployeeViewModel> CafeEmployees { get; set; } = new List<CafeEmployeeViewModel>();
    }
}
