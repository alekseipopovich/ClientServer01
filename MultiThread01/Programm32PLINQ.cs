using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm32PLINQ
    {
        static void Main32()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, };

           
            // с помощью операторов LINQ
            (from n in numbers.AsParallel() select Square(n)).ForAll(Console.WriteLine);

            // с помощью методов расширения LINQ
            numbers.AsParallel().Select(n => Square(n)).ForAll(Console.WriteLine);

            int Square(int n) => n * n;
        }
    }
}
