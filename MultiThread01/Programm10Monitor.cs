using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm10Monitor
    {
        static int x = 0;

        static object locker = new();  // объект-заглушка
        private static void Main10(string[] args)
        {
        
            // запускаем пять потоков
            for (int i = 1; i < 6; i++)
            {
                Thread myThread = new(PrintMonitor);
                myThread.Name = $"Поток {i}";   // устанавливаем имя для каждого потока
                myThread.Start();
            }
        }

        static void PrintMonitor()
        {
            bool acquiredLock = false;
            try
            {
                Monitor.Enter(locker, ref acquiredLock);
                x = 1;
                for (int i = 1; i < 6; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
                    x++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                if (acquiredLock) Monitor.Exit(locker);
            }
        }
    }
}
