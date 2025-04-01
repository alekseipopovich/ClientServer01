using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm11AutoReset
    {
        static int x = 0;

        //static object locker = new();  // объект-заглушка

        static AutoResetEvent waitHandler = new AutoResetEvent(true);  // объект-событие

        private static void Main11(string[] args)
        {

            // запускаем пять потоков
            for (int i = 1; i < 6; i++)
            {
                Thread myThread = new(PrintAutoReset);
                myThread.Name = $"Поток {i}";   // устанавливаем имя для каждого потока
                myThread.Start();
            }
        }

        static void PrintAutoReset()
        {
            waitHandler.WaitOne();  // ожидаем сигнала
            x = 1;
            for (int i = 1; i < 6; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                x++;
                Thread.Sleep(100);
            }
            waitHandler.Set();  //  сигнализируем, что waitHandler в сигнальном состоянии        }
        }
    }
}
