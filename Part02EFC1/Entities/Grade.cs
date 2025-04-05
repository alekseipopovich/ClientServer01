using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDataModel.Entities
{
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
}
