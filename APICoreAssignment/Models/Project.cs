using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Project
{
    public int ProjectId { get; set; }

    [Required, StringLength(10)]
    public string ProjectCode { get; set; }

    [Required, StringLength(100)]
    public string ProjectName { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    public decimal Budget { get; set; }

    public List<Employee>? Employees { get; set; }
}
