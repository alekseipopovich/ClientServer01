using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm17ParallelForEach
    {
        static void Main17()
        {
            ParallelLoopResult result = Parallel.ForEach<int>(
                new List<int>() { 1, 3, 5, 8 },
                Square
                );
        }

        // вычисляем квадрат числа
        static void Square(int n)
        {
            Console.WriteLine($"Выполняется задача {Task.CurrentId}");
            Console.WriteLine($"Квадрат числа {n} равен {n * n}");
            Thread.Sleep(2000);
        }
    }
}
