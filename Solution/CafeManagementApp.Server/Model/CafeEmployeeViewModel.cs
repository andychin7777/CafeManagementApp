using System.ComponentModel.DataAnnotations;

namespace CafeManagementApp.Server.Model
{
    public class CafeEmployeeViewModel
    {
        public long CafeEmployeeId { get; set; }

        [Required]
        public Guid CafeGuid { get; set; }

        [Required]
        public long EmployeeId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }


        // Navigation properties
        public CafeViewModel Cafe { get; set; }

        public EmployeeViewModel Employee { get; set; }
    }
}
