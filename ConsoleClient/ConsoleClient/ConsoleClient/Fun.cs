using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleClient
{
    public enum LogType
    { 
        Log = 1, Warn = 2, Error = 3
    }
    public class LogInfo
    {
        public int type;
        public string log;
    }
    internal class Fun
    {
        
    }
}
