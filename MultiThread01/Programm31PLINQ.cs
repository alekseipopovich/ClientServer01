using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm31PLINQ
    {
        static void Main32()
        {
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, };
            
            var squares = from n in numbers.AsParallel()
                          select Square(n);

            //var squares = numbers.AsParallel().Select(x => Square(x));

            foreach (var n in squares)
                Console.WriteLine(n);

            int Square(int n) => n * n;
        }
    }
}
