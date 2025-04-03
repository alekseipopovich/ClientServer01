using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class ProgrammLab05PLINQ
    {
        static void MainLab053(string[] args)
        {
            // Имитация данных о студентах и их оценках
            List<Grade> grades = new List<Grade>
            {
                new Grade { StudentName = "Вася", Subject = "Математика", Score = 90 },
                new Grade { StudentName = "Вася", Subject = "Физика", Score = 85 },
                new Grade { StudentName = "Петя", Subject = "Математика", Score = 75 },
                new Grade { StudentName = "Петя", Subject = "Физика", Score = 80 },
                new Grade { StudentName = "Коля", Subject = "Математика", Score = 95 },
                new Grade { StudentName = "Коля", Subject = "Физика", Score = 90 }
            };

            // Список студентов
            List<string> students = new List<string> { "Вася", "Петя", "Коля" };

            // Сервис для работы с данными
            var gradeService = new GradeService();

            // Замер времени выполнения
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Параллельная обработка с использованием PLINQ
            students.AsParallel().ForAll(student =>
            {
                double averageScore = gradeService.CalculateAverageScore(grades, student);
                Console.WriteLine($" Для студента {student} средняя оценка: {averageScore}");
            });


            watch.Stop();

            Console.WriteLine($"Все вычисления завершены. Время выполнения: {watch.ElapsedMilliseconds} мс");
        }

    }
}
