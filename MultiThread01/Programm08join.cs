using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm08join
    {
        private static void Main08(string[] args)
        {
            // Создаем несколько потоков
            Thread thread1 = new Thread(() => DoWork("Поток 1", 5));
            Thread thread2 = new Thread(() => DoWork("Поток 2", 3));
            Thread thread3 = new Thread(() => DoWork("Поток 3", 7));

            // Запускаем потоки
            thread1.Start();
            thread2.Start();
            thread3.Start();

            // Ожидаем завершения всех потоков
            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine("Все потоки завершили работу");
        }

        static void DoWork(string threadName, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine($"{threadName}: итерация {i}");
                Thread.Sleep(500); // Имитация работы
            }
        }
    }
}
