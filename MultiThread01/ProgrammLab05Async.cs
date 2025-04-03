using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class ProgrammLab05Async
    {
        static async Task MainLab05a(string[] args)
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
            var gradeService = new GradeServiceAsync();

            // Замер времени выполнения
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Асинхронно вычисляем средний балл для каждого студента
            var calculationTasks = students.Select(student =>
                gradeService.CalculateAverageScoreAsync(grades, student));

            var results = await Task.WhenAll(calculationTasks);

            // Выводим результаты
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"Для студента {students[i]} средняя оценка: {results[i]}");
            }

            watch.Stop();
            Console.WriteLine($"Все вычисления завершены. Время выполнения: {watch.ElapsedMilliseconds} мс");
        }
    }

    public class GradeServiceAsync
    {
        // Асинхронный метод для расчета среднего балла студента
        public async Task<double> CalculateAverageScoreAsync(List<Grade> grades, string studentName)
        {
            // Имитация асинхронной операции (например, запроса к базе данных)
            await Task.Delay(100); // Задержка для имитации долгой операции

            var studentGrades = grades.Where(g => g.StudentName == studentName).ToList();

            if (studentGrades.Count == 0)
                return 0;

            double totalScore = studentGrades.Sum(g => g.Score);
            return totalScore / studentGrades.Count;
        }
    }
}
