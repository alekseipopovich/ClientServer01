using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm35PLINQ
    {
        static void Main35()
        {
            string[] words = { "яблоко", "банан", "апельсин", "виноград", "киви" };

            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"'{word}' имеет длину {word.Length} (поток {Task.CurrentId})");
            });
        }
    }
}
