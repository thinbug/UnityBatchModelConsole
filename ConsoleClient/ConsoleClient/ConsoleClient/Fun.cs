using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    internal class Fun
    {
        public static void Log(string log)
        { 
            Console.WriteLine(log);
        }
        public static void Warn(string log)
        {
            Console.WriteLine(log);
        }
        public static void Error(string log)
        {
            Console.WriteLine(log);
        }
    }
}
