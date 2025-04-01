using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm03Thread
    {

        private static void Main03(string[] args)
        {
            Thread myThread = new Thread(new ThreadStart(Count));
            myThread.Start();
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Главный поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(300);
            }

        }

        public static void Count()
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(400);
            }
        }


    }
}
