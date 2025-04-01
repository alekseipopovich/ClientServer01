using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThread01
{
    class Programm24ValueTask
    {
        static async Task Main24()
        {
            var addAsync = (int a, int b) => new ValueTask<int>(a + b);

            var result = await addAsync(4, 5);
            Console.WriteLine(result);
        }

    }
}
