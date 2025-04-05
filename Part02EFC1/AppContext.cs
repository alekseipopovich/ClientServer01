using EntityDataModel.Entities;
using Microsoft.EntityFrameworkCore;
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

        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sqlite20250404.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Grade>().HasIndex(u => u.StudentID).IsUnique();
        }
    }
}
