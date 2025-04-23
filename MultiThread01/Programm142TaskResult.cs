using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part01MultiThread
{
    class Programm142TaskResult
    {
        static void Main142()
        {
            // Список студентов
            var students = new List<Student>
        {
            new Student { Name = "Анна", Grades = new[] { 90, 85, 88 } },
            new Student { Name = "Борис", Grades = new[] { 78, 82, 80 } },
            new Student { Name = "Виктор", Grades = new[] { 95, 92, 89 } },
            new Student { Name = "Дарья", Grades = new[] { 87, 91, 86 } }
        };

            // Создаем задачи
            var tasks = new List<Task>();
            foreach (var student in students)
            {
                var task = Task.Run(() =>
                {
                    double average = CalculateAverage(student.Grades);
                    Console.WriteLine($"{student.Name}: Средний балл = {average:F2}");
                });
                tasks.Add(task);
            }

            // Ждем завершения всех задач
            Task.WaitAll(tasks.ToArray());
        }

        static double CalculateAverage(int[] grades)
        {
            return grades.Any() ? grades.Average() : 0;
        }
    }

    class Student
    {
        public string Name { get; set; }
        public int[] Grades { get; set; }
    }
}
