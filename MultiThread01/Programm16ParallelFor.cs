using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm16ParallelFor
    {
        static void Main16()
        {
            Parallel.For(1, 5, Square);
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
