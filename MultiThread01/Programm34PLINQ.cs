using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm34PLINQ
    {
        static void Main34()
        {
            //Parallel.For(0, 10, i => {
            //    Console.WriteLine($"Обработка элемента {i} в потоке {Thread.CurrentThread.ManagedThreadId}");
            //});

            var numbers = Enumerable.Range(1, 10);
            Parallel.ForEach(numbers, number => {
                Console.WriteLine($"Квадрат числа {number} = {number * number}");
            });
        }
    }
}
