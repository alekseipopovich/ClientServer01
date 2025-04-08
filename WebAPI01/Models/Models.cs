namespace GradesApi.Models;

public class Student
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Grade> Grades { get; set; } = new();
}

public class Subject
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Grade> Grades { get; set; } = new();
}

public class Grade
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int Score { get; set; }
    public DateOnly Date { get; set; }
    
    public Student? Student { get; set; }
    public Subject? Subject { get; set; }
} 