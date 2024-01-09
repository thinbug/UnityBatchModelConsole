using System;
using UnityEngine;
namespace WindowsConsoleMine
{
    public class ConsoleDebugLogCallback : MonoBehaviour
    {
        string timeFormat = "yyyy-MM-dd HH:mm:ss";
        private void Awake()
        {
            Application.logMessageReceived += LogMessageReceived;
        }


        private void LogMessageReceived(string condition, string stackTrace, LogType type)
        {
            string logHead = $"[{DateTime.Now.ToString(timeFormat)}]";
            string sOut = "";
            switch (type)
            {
                case LogType.Log:
                    Console.ForegroundColor = ConsoleColor.White;
                    sOut = logHead + condition;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    sOut = logHead + condition;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    sOut = logHead + condition +"\n" + stackTrace;
                    break;
            }
            Console.WriteLine(sOut);
        }

    }
}