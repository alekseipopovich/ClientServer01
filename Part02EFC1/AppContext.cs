using EntityDataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework01
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;

        public DbSet<Course> Courses { get; set; } = null!;

        public DbSet<Grade> Grades { get; set; } = null!;

        public DbSet<Enrollment> Enrollments { get; set; } = null!;

        public ApplicationContext() { Database.EnsureDeleted(); Database.EnsureCreated(); }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=sqlite20250404.db");
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            //optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Warning);

            // Включение ленивой загрузки
            optionsBuilder.UseLazyLoadingProxies().UseSqlite(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Grade>().HasIndex(u => u.StudentID).IsUnique();

            modelBuilder.Entity<Enrollment>()
               .HasKey(e => new { e.StudentId, e.CourseId });

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

        }

        public void SeedData()
        {
            if (!Students.Any() && !Courses.Any() && !Grades.Any())
            {
                Student s1 = new Student { FirstName = "Vasya", LastName = "Pupkin", Age = 20, Address = "Moskow" };
                Student s2 = new Student { FirstName = "Ivan", LastName = "Ivanov", Age = 25, Address = "Bratsk" };
                Student s3 = new Student { FirstName = "Petr", LastName = "Petrov", Age = 35, Address = "Irkutsk" };

                Students.Add(s1);
                Students.Add(s2);

                Course course1 = new Course { CourseName = "Математика" };
                Course course2 = new Course { CourseName = "Физика" };

                Courses.Add(course1);
                Courses.Add(course2);

                Grades.Add(new Grade { Student = s1, Course = course1, Score = 4 });
                Grades.Add(new Grade { Student = s2, Course = course2, Score = 3 });

                SaveChanges();
            }
        }

        public void UpdateStudent(int studentId, string newFirstName, string newLastName, int newAge, string newAddress)
        {
            var student = Students.Find(studentId);
            if (student != null)
            {
                student.FirstName = newFirstName;
                student.LastName = newLastName;
                student.Age = newAge;
                student.Address = newAddress;
                SaveChanges();
            }
        }

        public void DeleteStudent(int studentId)
        {
            var student = Students.Find(studentId);
            if (student != null)
            {
                Students.Remove(student);
                SaveChanges();
            }
        }
    }
}
