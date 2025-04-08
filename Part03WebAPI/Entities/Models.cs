using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities;

public class Enrollment
{
    [Key]
    public int EnrollmentId { get; set; }

    [ForeignKey("Student")]
    public int StudentId { get; set; }
    public virtual Student Student { get; set; }

    [ForeignKey("Course")]
    public int CourseId { get; set; }
    public virtual Course Course { get; set; }
}

public class Course
{
    [Key]
    public int CourseId { get; set; }
        
    [Required]
    public string? CourseName { get; set; }

    public virtual ICollection<Grade>? Grades { get; set; } = new List<Grade>();
        
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}

public class Student
{
    [Key]
    public int StudentId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? Address { get; set; }

    public virtual ICollection<Grade>? Grades { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

}

public class Grade
{
    [Key]
    public int GradeID { get; set; }

    [ForeignKey("Student")]
    public int StudentID { get; set; }

    [ForeignKey("Course")]
    public int CourseID { get; set; }

    [Required]
    public decimal Score { get; set; }

    public virtual Student? Student { get; set; }
    public virtual Course? Course { get; set; }
}