using System;
using PrimeTestKalle.Extensions;

namespace PrimeTestKalle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The Prime Tester!");
            Console.WriteLine("-----------------");

            var n1 = 0;
            var n2 = 0;
            var validArguments = false;

            if (args != null && args.Length == 2)
            {
                if (args[0].IsNumeric() && args[1].IsNumeric())
                {
                    n1 = args[0].ToInt32();
                    n2 = args[1].ToInt32();
                    validArguments = true;
                }
            }

            if (validArguments)
            {
                var primeTester = new PrimeTester();
                primeTester.TestSpan(n1, n2);
                primeTester = null;
            }
            else
            {
                Console.WriteLine("Missing arguments!\n\rExpects two numbers to be a span!");
            }


            Console.WriteLine("Press any key to exit!");
            Console.ReadKey(true);
            
            Console.WriteLine("Bye...");
        }
    }
}
