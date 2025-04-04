using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Part02EFC0;

public partial class AppContext1 : DbContext
{
    public AppContext1()
    {
    }

    public AppContext1(DbContextOptions<AppContext1> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

        optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
    }
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlite("Data Source = D:\\projects\\ClientServer01\\Part02EFC1\\sqlite20250404.db");
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasIndex(e => e.CourseId, "IX_Grades_CourseID");

            entity.HasIndex(e => e.StudentId, "IX_Grades_StudentID");

            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.Grades).HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Student).WithMany(p => p.Grades).HasForeignKey(d => d.StudentId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
