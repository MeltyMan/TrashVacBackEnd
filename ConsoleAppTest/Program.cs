using System;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstName = string.Empty;
            var lastName = "";

            if (args != null && args.Length == 2)
            {
                firstName = args[0];
                lastName = args[1];
            }
            else
            {
                Console.WriteLine("Missing arguments!");
                return;
            }

            var theProcess = new TheProcess();

            theProcess.DoTheJob(firstName, lastName);

            theProcess = null;
        }
    }
}
