using System;
using System.Collections.Generic;

namespace Part02EFC0;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
