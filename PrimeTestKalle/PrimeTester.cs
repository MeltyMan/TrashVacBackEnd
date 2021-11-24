using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeTestKalle.Extensions;

namespace PrimeTestKalle
{
    public class PrimeTester
    {
        private IList<int> _primes = new List<int>();

        public void TestSpan(int n1, int n2)
        {
            var startTime = DateTime.Now;

            Console.WriteLine($"Checking for prime's in the span {n1} to {n2}");
            var current = 0;
            var total = n2 - n1;
            WriteProgress(current, total);
            for (var n = n1; n <= n2; n++)
            {
                current++;
                WriteProgress(current, total);
                if (n.IsPrime())
                {
                    _primes.Add(n);
                }
            }

            var endTime = DateTime.Now;

            Console.WriteLine($"\n\rDone! Found {_primes.Count} prime(s) in the span!");
            Console.WriteLine($"This took {(endTime - startTime).TotalMilliseconds} milliseconds to complete");
            Console.WriteLine("Do you want to see the results? (y/n)");
            var q = Console.ReadKey(true);
            if (q.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Found these prime(s):");
                foreach (var prime in _primes)
                {
                    Console.WriteLine(prime);
                }
            }

        }

        private void WriteProgress(int current, int total)
        {
            var perc = ((double)current / (double)total) * 100;
            if (perc > 100)
            {
                perc = 100;
            }

            
            Console.Write($"\rProgress: {Math.Round(perc, 2)}%     ");


        }
    }
}
