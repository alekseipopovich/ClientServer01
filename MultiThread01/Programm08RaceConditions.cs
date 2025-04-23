using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part01MultiThread
{
    class Programm08RaceConditions
    {
        static int totalGrades = 0; // Общий ресурс
        static int gradeCount = 0;
        
        static void Main082()
        {
            Thread thread1 = new Thread(AddGrade);
            Thread thread2 = new Thread(AddGrade);

            thread1.Start(10);
            thread2.Start(85);

            thread1.Join();
            thread2.Join();

            Console.WriteLine($"Средний балл: {(double)totalGrades / gradeCount}");
        }

        static void Main083()
        {
            Thread[] threads = new Thread[5];
            int[] grades = { 90, 85, 88, 92, 87 };

            for (int i = 0; i < 5; i++)
            {
                threads[i] = new Thread(AddGrade);
                threads[i].Start(grades[i]);
            }

            foreach (var t in threads) t.Join();

            Console.WriteLine($"Сумма оценок: {totalGrades}, Количество: {gradeCount}");
            Console.WriteLine($"Средний балл: {(double)totalGrades / gradeCount:F2}");
        }


        static void AddGrade(object grade)
        {
            totalGrades += (int)grade; // Условие гонки
            gradeCount++;              // Условие гонки

            //int temp = totalGrades; // Читаем
            //Thread.Sleep(1); // Имитация задержки
            //totalGrades = temp + (int)grade; // Записываем

            //temp = gradeCount;
            //Thread.Sleep(1);
            //gradeCount = temp + 1;
        }
    }
}
