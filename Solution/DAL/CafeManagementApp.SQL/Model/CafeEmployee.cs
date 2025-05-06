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
        public Guid CafeId { get; set; }

        [Required]
        [ForeignKey(nameof(Model.Employee))]
        public string EmployeeId { get; set; }

        // Navigation properties
        public Cafe Cafe { get; set; }

        public Employee Employee { get; set; }
    }
}
