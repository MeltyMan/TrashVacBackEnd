using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public class TheProcess
    {
        public void DoTheJob(string firstName, string lastName)
        {
            Console.WriteLine($"Hej hej {firstName} {lastName}");

            Console.WriteLine("Press any key to exit");

            Console.ReadKey(true);
        }
    }
}
