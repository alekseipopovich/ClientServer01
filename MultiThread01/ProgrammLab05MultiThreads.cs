using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class ProgrammLab05MultiThreads
    {
        static void MainLab05Multi(string[] args)
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

            // Создаем и запускаем потоки для каждого студента, который рассчитывает его средний балл.
            List<Thread> threads = new List<Thread>();
            foreach (var student in students)
            {
                Thread thread = new Thread(() =>
                {
                    double averageScore = gradeService.CalculateAverageScore(grades, student);
                    Console.WriteLine($" Для студента {student} средняя оценка: {averageScore}");
                });
                threads.Add(thread);
                thread.Start();
            }

            // Ожидаем завершения всех потоков
            foreach (var thread in threads)
            {
                // используется метод Join() для каждого потока, чтобы дождаться их завершения.
                thread.Join();
            }

            watch.Stop();

            Console.WriteLine($"Все вычисления завершены. Время выполнения: {watch.ElapsedMilliseconds} мс");
        }

    }
}
