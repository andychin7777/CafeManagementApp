using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeManagementApp.SQL.Model
{
    public class CafeEmployee
    {
        [Key]
        public long CafeEmployeeId { get; set; }

        [Required]
        [ForeignKey(nameof(Model.Cafe))]
        public Guid CafeGuid { get; set; }

        [Required]
        [ForeignKey(nameof(Model.Employee))]
        public long EmployeeId { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        // Navigation properties
        public Cafe Cafe { get; set; }

        public Employee Employee { get; set; }
    }
}
