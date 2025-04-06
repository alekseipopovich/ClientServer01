using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDataModel.Entities;

public class Student
{
    [Key]
    public int StudentId { get; set; }
    [Required]
    public string? FirstName { get; set; } // Имя
    [Required]
    public string? LastName { get; set; }  // Фамилия
    public int Age { get; set; }          // Возраст
    public string? Address { get; set; }   // Адрес

    public virtual ICollection<Grade>? Grades { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}
