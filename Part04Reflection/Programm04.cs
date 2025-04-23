using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Part04Reflection
{
    public class Programm04
    {
        static void Main()
        {

            Person p = new Person("Alex");

            p.Print();

            var number = 5;
            var result = Square(number);
            Console.WriteLine($"Квадрат {number} равен {result}");
        }

        static int Square(int n) => n * n;
    }
}
