using System.ComponentModel.DataAnnotations;

namespace EntityDataModel.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        
        [Required]
        public string? CourseName { get; set; }

        public virtual ICollection<Grade>? Grades { get; set; } = new List<Grade>();
        
    }
}
