using EntityDataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework01
{
    class Programm
    {
        public static void Main()
        {
            using (ApplicationContext db = new())
            {
                // Инициализация данных, если они отсутствуют
                db.SeedData();

                Console.WriteLine("Выполнение запросов SQL:");

                var studentsSQL = db.Students.FromSqlRaw<Student>("SELECT * FROM Students").ToList();

                //db.Database.ExecuteSqlRaw("UPDATE Student SET FirstName='Kolya' WHERE Lastname='Pupkin'");
                foreach(Student s in studentsSQL) { Console.WriteLine(s.LastName); }


                // Eager Loading: Извлечение информации о курсах и студентах
                var coursesWithStudents = db.Courses
                    .Include(c => c.Enrollments)
                    .ThenInclude(e => e.Student)
                    .ToList();

                Console.WriteLine("\nКурсы и студенты:");
                foreach (var course in coursesWithStudents)
                {
                    Console.WriteLine($"Курс: {course.CourseName}");
                    foreach (var enrollment in course.Enrollments)
                    {
                        Console.WriteLine($"  Студент: {enrollment.Student.FirstName} {enrollment.Student.LastName}");
                    }
                }

                // Eager Loading: Извлечение информации о студентах и курсах
                var studentsWithCourses = db.Students
                    .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                    .ToList();

                Console.WriteLine("\nСтуденты и курсы:");
                foreach (var student in studentsWithCourses)
                {
                    Console.WriteLine($"Студент: {student.FirstName} {student.LastName}");
                    foreach (var enrollment in student.Enrollments)
                    {
                        Console.WriteLine($"  Курс: {enrollment.Course.CourseName}");
                    }
                }





                // Обновление данных студента
                var studentToUpdate = studentsWithCourses.FirstOrDefault();
                if (studentToUpdate != null)
                {
                    db.UpdateStudent(studentToUpdate.StudentId, "Вася", "Пупкин", 25, "Иркутск");
                }

                // Удаление студента
                var studentToDelete = studentsWithCourses.LastOrDefault();
                if (studentToDelete != null)
                {
                    db.DeleteStudent(studentToDelete.StudentId);
                }

                // Повторный вывод списка студентов после обновления и удаления
                studentsWithCourses = db.Students
                    .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                    .ToList();
                Console.WriteLine("\nСписок студентов после обновления и удаления:");
                foreach (var student in studentsWithCourses)
                {
                    Console.WriteLine($"Студент: {student.FirstName} {student.LastName}");
                    foreach (var enrollment in student.Enrollments)
                    {
                        Console.WriteLine($"  Курс: {enrollment.Course.CourseName}");
                    }
                }

                // Запрос 2: Получить оценки студента по имени "Иван"
                var ivanGrades = db.Grades
                    .Where(g => g.Student.FirstName == "Ivan")
                    .Select(g => new
                    {
                        CourseName = g.Course.CourseName,
                        Score = g.Score
                    })
                    .ToList();

                Console.WriteLine("\nОценки Ивана:");
                foreach (var grade in ivanGrades)
                {
                    Console.WriteLine($"{grade.CourseName}: {grade.Score}");
                }

                // Запрос 3: Получить средний балл по каждому курсу
                var averageScores = db.Grades
                    .GroupBy(g => g.Course.CourseName)
                    .Select(g => new
                    {
                        CourseName = g.Key,
                        AverageScore = g.Average(gr => gr.Score)
                    })
                    .ToList();

                Console.WriteLine("\nСредний балл по курсам:");
                foreach (var item in averageScores)
                {
                    Console.WriteLine($"{item.CourseName}: {item.AverageScore:F2}");
                }

                Console.WriteLine();
            }


            using (ApplicationContext db = new())
            {
                // Инициализация данных, если они отсутствуют
                db.SeedData();

                Console.WriteLine("--- ЛЕНИВАЯ ЗАГРУЗКА ---");

                // Пример ленивой загрузки
                var studentLazy = db.Students.OrderBy(s => s.StudentId).FirstOrDefault();
                if (studentLazy != null)
                {
                    Console.WriteLine($"Студент: {studentLazy.FirstName} {studentLazy.LastName}");
                    foreach (var enrollment in studentLazy.Enrollments)
                    {
                        Console.WriteLine($"  Курс: {enrollment.Course.CourseName}");
                    }
                }

                Console.WriteLine();
            }

            using (ApplicationContext db = new())
            {
                // Инициализация данных, если они отсутствуют
                db.SeedData();

                Console.WriteLine("--- ЯВНАЯ ЗАГРУЗКА ---");

                // Явная загрузка: Извлечение информации о курсах и студентах
                var course = db.Courses.OrderBy(c => c.CourseId).FirstOrDefault();
                if (course != null)
                {
                    Console.WriteLine($"Курс: {course.CourseName}");

                    // Явная загрузка связанных данных
                    db.Entry(course).Collection(c => c.Enrollments).Load();
                    foreach (var enrollment in course.Enrollments)
                    {
                        db.Entry(enrollment).Reference(e => e.Student).Load();
                        Console.WriteLine($"  Студент: {enrollment.Student.FirstName} {enrollment.Student.LastName}");
                    }
                }

                // Явная загрузка: Извлечение информации о студентах и курсах
                var student = db.Students.OrderBy(s => s.StudentId).FirstOrDefault();
                if (student != null)
                {
                    Console.WriteLine($"\nСтудент: {student.FirstName} {student.LastName}");

                    // Явная загрузка связанных данных
                    db.Entry(student).Collection(s => s.Enrollments).Load();
                    foreach (var enrollment in student.Enrollments)
                    {
                        db.Entry(enrollment).Reference(e => e.Course).Load();
                        Console.WriteLine($"  Курс: {enrollment.Course.CourseName}");
                    }
                }
            }
        }
    }
}
