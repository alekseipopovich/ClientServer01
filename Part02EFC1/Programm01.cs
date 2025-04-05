using EntityDataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework01
{
    class Programm01
    {
        public static void Main()
        {
            using (ApplicationContext db = new())
            {
                //Student s1 = db.Students.FirstOrDefault(c => c.FirstName == "Vasya" && c.LastName == "Pupkin")
                 //   ?? new Student { FirstName = "Vasya", LastName = "Pupkin", Age = 20, Address = "Moskow" };
                
                /*
                Student student1 = new Student { FirstName = "Вася", LastName = "Пупкин", Age = 22 };
                Student student2 = new Student { FirstName = "Коля", LastName = "Иванов", Age = 18 };

                db.Add(student1); db.Add(student2);

                Course course1 = new Course { CourseName = "Математика" };
                Course course2 = new Course { CourseName = "Физика" };

                db.Courses.Add(course1); db.Courses.Add(course2);

                var grade1 = db.Grades.Add(new Grade { Student = student1, Course = course1, Score = 4 });

                var grade2 = db.Grades.Add(new Grade { Student = student2, Course = course2, Score = 3 });

                try
                {
                    db.SaveChanges();

                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine("Проблемы с обновлением данных");
                }
                */

                var students = db.Students.ToList();

                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName} - {student.Age}");
                }

    
            }
        }
    }
}
