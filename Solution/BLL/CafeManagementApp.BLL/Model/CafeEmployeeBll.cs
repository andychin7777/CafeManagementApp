using System.ComponentModel.DataAnnotations;

namespace CafeManagementApp.BLL.Model
{
    public class CafeEmployeeBll
    {
        public long CafeEmployeeId { get; set; }

        [Required]
        public Guid CafeId { get; set; }

        [Required]
        public long EmployeeId { get; set; }

        public DateOnly? StartDate { get; set; }


        // Navigation properties
        public CafeBll Cafe { get; set; }

        public EmployeeBll Employee { get; set; }
    }
}
