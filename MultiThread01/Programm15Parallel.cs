using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm15Parallel
    {
        static void Main15()
        {
            // метод Parallel.Invoke выполняет три метода
            Parallel.Invoke(
                Print,
                () =>
                {
                    Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                    Thread.Sleep(3000);
                },
                () => Square(5)
            );

            void Print()
            {
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Thread.Sleep(3000);
            }
            // вычисляем квадрат числа
            void Square(int n)
            {
                Console.WriteLine($"Выполняется задача {Task.CurrentId}");
                Thread.Sleep(3000);
                Console.WriteLine($"Результат {n * n}");
            }
        }
    }
}
