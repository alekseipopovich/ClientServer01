using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using GradesApi.Data;
using GradesApi.Models;

namespace GradesApi.Endpoints;

public static class SubjectEndpoints
{
    public static void MapSubjectEndpoints(this WebApplication app)
    {
        app.MapGet("/subjects", async (ApplicationDbContext db) => 
            await db.Subjects.ToListAsync())
            .WithName("GetAllSubjects");

        app.MapGet("/subjects/{id}", async (int id, ApplicationDbContext db) => 
            await db.Subjects.FindAsync(id) is Subject subject
                ? Results.Ok(subject)
                : Results.NotFound())
            .WithName("GetSubject");

        app.MapPost("/subjects", async (Subject subject, ApplicationDbContext db) =>
        {
            db.Subjects.Add(subject);
            await db.SaveChangesAsync();
            return Results.Created($"/subjects/{subject.Id}", subject);
        })
        .WithName("AddSubject");

        app.MapPut("/subjects/{id}", async (int id, Subject subject, ApplicationDbContext db) =>
        {
            var existingSubject = await db.Subjects.FindAsync(id);
            if (existingSubject == null) return Results.NotFound();
            
            existingSubject.Name = subject.Name;
            await db.SaveChangesAsync();
            return Results.Ok(existingSubject);
        })
        .WithName("UpdateSubject");

        app.MapDelete("/subjects/{id}", async (int id, ApplicationDbContext db) =>
        {
            var subject = await db.Subjects.FindAsync(id);
            if (subject == null) return Results.NotFound();
            
            db.Subjects.Remove(subject);
            await db.SaveChangesAsync();
            return Results.Ok(subject);
        })
        .WithName("DeleteSubject");
    }
} 