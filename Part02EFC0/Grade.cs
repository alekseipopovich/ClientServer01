using System;
using System.Collections.Generic;

namespace Part02EFC0;

public partial class Grade
{
    public int GradeId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public decimal Score { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
