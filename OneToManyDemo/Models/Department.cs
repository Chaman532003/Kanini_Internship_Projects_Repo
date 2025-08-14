using System.ComponentModel.DataAnnotations;

namespace OneToManyDemo.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public IList<Employee> Employees { get; set; }
    }
}
