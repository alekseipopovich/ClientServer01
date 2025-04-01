using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class ProgrammLab01
    {
        static void MainLab01()
        {
            Console.WriteLine("Основной поток начал работу");

            // Создаем и запускаем потоки с помощью лямбда-выражений
            Thread thread1 = new Thread(() => PrintNumbers(5, 15));
            Thread thread2 = new Thread(() => PrintNumbers(10, 20));

            thread1.Start();
            thread2.Start();

            //thread1.Join();
            //thread2.Join();

            Console.WriteLine("Основной поток завершил работу");
        }

        static void PrintNumbers(int start, int end)
        {
            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} начал работу");

            for (int i = start; i <= end; i++)
            {
                Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: {i}");
                Thread.Sleep(100); // Имитация работы
            }

            Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId} завершил работу");
        }
    }
}
