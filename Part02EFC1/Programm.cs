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

                var allStudents = db.Students.ToList();

                Console.WriteLine("Список студентов:");
                foreach (var student in allStudents)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName}");
                }

                // Обновление данных студента
                var studentToUpdate = allStudents.FirstOrDefault();
                if (studentToUpdate != null)
                {
                    db.UpdateStudent(studentToUpdate.StudentId, "Вася", "Пупкин", 25, "Иркутск");
                }

                // Удаление студента
                var studentToDelete = allStudents.LastOrDefault();
                if (studentToDelete != null)
                {
                    db.DeleteStudent(studentToDelete.StudentId);
                }

                // Повторный вывод списка студентов после обновления и удаления
                allStudents = db.Students.ToList();
                Console.WriteLine("\nСписок студентов после обновления и удаления:");
                foreach (var student in allStudents)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName}");
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
            }
        }
    
    
    

    }
}
