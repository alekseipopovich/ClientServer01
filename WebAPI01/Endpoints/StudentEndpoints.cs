using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using GradesApi.Data;
using GradesApi.Models;

namespace GradesApi.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this WebApplication app)
    {
        app.MapGet("/students", async (ApplicationDbContext db) => 
            await db.Students.ToListAsync())
            .WithName("GetAllStudents");

        app.MapGet("/students/{id}", async (int id, ApplicationDbContext db) => 
            await db.Students.FindAsync(id) is Student student
                ? Results.Ok(student)
                : Results.NotFound())
            .WithName("GetStudent");

        app.MapPost("/students", async (Student student, ApplicationDbContext db) =>
        {
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return Results.Created($"/students/{student.Id}", student);
        })
        .WithName("AddStudent");

        app.MapPut("/students/{id}", async (int id, Student student, ApplicationDbContext db) =>
        {
            var existingStudent = await db.Students.FindAsync(id);
            if (existingStudent == null) return Results.NotFound();
            
            existingStudent.Name = student.Name;
            await db.SaveChangesAsync();
            return Results.Ok(existingStudent);
        })
        .WithName("UpdateStudent");

        app.MapDelete("/students/{id}", async (int id, ApplicationDbContext db) =>
        {
            var student = await db.Students.FindAsync(id);
            if (student == null) return Results.NotFound();
            
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return Results.Ok(student);
        })
        .WithName("DeleteStudent");
    }
} 