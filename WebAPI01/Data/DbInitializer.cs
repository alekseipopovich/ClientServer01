using Microsoft.EntityFrameworkCore;
using GradesApi.Models;

namespace GradesApi.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext db)
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        // Добавляем тестовые данные
        if (!db.Students.Any())
        {
            db.Students.AddRange(
                new Student { Name = "Иван Иванов" },
                new Student { Name = "Петр Петров" }
            );
            db.SaveChanges();
        }

        if (!db.Subjects.Any())
        {
            db.Subjects.AddRange(
                new Subject { Name = "Математика" },
                new Subject { Name = "Физика" }
            );
            db.SaveChanges();
        }

        if (!db.Grades.Any())
        {
            var student1 = db.Students.First();
            var student2 = db.Students.Skip(1).First();
            var math = db.Subjects.First();
            var physics = db.Subjects.Skip(1).First();

            db.Grades.AddRange(
                new Grade { StudentId = student1.Id, SubjectId = math.Id, Score = 5, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-5)) },
                new Grade { StudentId = student1.Id, SubjectId = physics.Id, Score = 4, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-3)) },
                new Grade { StudentId = student2.Id, SubjectId = math.Id, Score = 4, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)) }
            );
            db.SaveChanges();
        }
    }
} 