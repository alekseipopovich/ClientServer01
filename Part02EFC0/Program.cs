using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Part02EFC0;

using (AppContext1 db = new())
{
    var students = db.Students.ToList();

    foreach (var student in students)
    {
        Console.WriteLine($"{student.FirstName} {student.LastName} - {student.Age}");
    }
}
