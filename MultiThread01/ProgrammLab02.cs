using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class ProgrammLab02
    {
        static Thread? thread1;

        static void MainLab02()
        {
            Console.WriteLine("Основной поток начал работу");

            // Первый эксперимент - без задержки
            Console.WriteLine("\nЭксперимент 1: Без задержки между запусками");
            thread1 = new Thread(PrintNumbersFirst);
            Thread thread2 = new Thread(PrintNumbersSecond);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            // Второй эксперимент - с задержкой
            Console.WriteLine("\nЭксперимент 2: С задержкой 1с между запусками");
            thread1 = new Thread(PrintNumbersFirst);
            thread2 = new Thread(PrintNumbersSecond);

            thread1.Start();
            Thread.Sleep(1000); // Задержка 1 секунда
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("\nОсновной поток завершил работу");
        }

        static void PrintNumbersFirst()
        {
            Console.WriteLine("Поток 1 начал работу");
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine($"Поток 1: {i}");
            }
            Console.WriteLine("Поток 1 завершил работу");
        }

        static void PrintNumbersSecond()
        {
            Console.WriteLine("Поток 2 ожидает завершения потока 1...");
            //thread1.Join(); // Ждем завершения первого потока

            Console.WriteLine("Поток 2 начал работу");
            for (int i = 1; i <= 100; i++)
            {
                Console.WriteLine($"Поток 2: {i}");
            }
            Console.WriteLine("Поток 2 завершил работу");
        }
    }
}
