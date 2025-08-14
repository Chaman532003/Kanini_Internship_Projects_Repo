using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneToManyDemo.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
