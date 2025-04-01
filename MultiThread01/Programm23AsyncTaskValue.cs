using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm23AsyncTaskValue
    {
        static async Task Main23()
        {
            int n1 = await SquareAsync(5);
            int n2 = await SquareAsync(6);
            Console.WriteLine($"n1={n1}  n2={n2}"); // n1=25  n2=36
        }

        static async Task<int> SquareAsync(int n)
        {
            await Task.Delay(0);
            return n * n;
        }
    }

}
