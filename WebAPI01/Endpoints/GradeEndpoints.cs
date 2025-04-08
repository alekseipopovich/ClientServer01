using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using GradesApi.Data;
using GradesApi.Models;

namespace GradesApi.Endpoints;

public static class GradeEndpoints
{
    public static void MapGradeEndpoints(this WebApplication app)
    {
        app.MapGet("/grades", async (ApplicationDbContext db) => 
            await db.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .Select(g => new {
                    Id = g.Id,
                    StudentName = g.Student!.Name,
                    SubjectName = g.Subject!.Name,
                    Score = g.Score,
                    Date = g.Date
                })
                .ToListAsync())
            .WithName("GetAllGrades");

        app.MapGet("/grades/student/{studentId}", async (int studentId, ApplicationDbContext db) =>
            await db.Grades
                .Where(g => g.StudentId == studentId)
                .Include(g => g.Subject)
                .Select(g => new {
                    SubjectName = g.Subject!.Name,
                    Score = g.Score,
                    Date = g.Date
                })
                .ToListAsync())
            .WithName("GetStudentGrades");

        app.MapPost("/grades", async (Grade grade, ApplicationDbContext db) =>
        {
            // Проверяем существование студента и предмета
            var student = await db.Students.FindAsync(grade.StudentId);
            var subject = await db.Subjects.FindAsync(grade.SubjectId);
            
            if (student == null)
                return Results.NotFound($"Student with ID {grade.StudentId} not found");
            if (subject == null)
                return Results.NotFound($"Subject with ID {grade.SubjectId} not found");

            db.Grades.Add(grade);
            await db.SaveChangesAsync();

            // Загружаем созданную оценку вместе с связанными данными
            var createdGrade = await db.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .FirstOrDefaultAsync(g => g.Id == grade.Id);

            return Results.Created($"/grades/{grade.Id}", createdGrade);
        })
        .WithName("AddGrade");
    }
} 