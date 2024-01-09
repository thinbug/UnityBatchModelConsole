using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Security;
using System.Text;

namespace WindowsConsoleMine
{
    //public delegate bool ControlCtrlDelegate(int CtrlType);

    /// <summary>
    /// Creates a console window that actually works in Unity
    /// You should add a script that redirects output using Console.Write to write to it.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public class ConsoleWindow
    {
        

        public static ConsoleWindow inst;
        TextWriter oldOutput;
 
        //public static bool HandlerRoutineMethod(int CtrlType)
        //{
        //    Console.WriteLine(CtrlType);

        //    switch (CtrlType)
        //    {
        //        case CTRL_C_EVENT:
                    
        //            Console.WriteLine("0���߱�ǿ�ƹر�"); //Ctrl+C�ر�  
        //            //��ش���ִ��
        //            return false;
                    
        //        case CTRL_CLOSE_EVENT:
        //            Console.WriteLine("ȷʵҪ�˳�����ô�������Ҫ�˳���������'exit'��");
        //            return false;
        //    }
        //    Debug.Log("ȷʵҪ�˳�����ô CtrlType:" + CtrlType);
        //    return false;
        //}
        public ConsoleWindow()
        { 
            inst = this;
            Initialize();

            //if (!SetConsoleCtrlHandler(HandlerRoutineMethod, true))
            //{
            //    Console.WriteLine("�޷�ע��ϵͳ�¼�!\n");
            //}
        }
        

        public void Initialize()
        {

            //
            // Attach to any existing consoles we have
            // failing that, create a new one.
            //
            if (!AttachConsole(0x0ffffffff))
            {
                AllocConsole();
            }

            oldOutput = Console.Out;
            Encoding encoding = Encoding.GetEncoding(936);
            try
            {
                
                IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);

              

                Microsoft.Win32.SafeHandles.SafeFileHandle safeFileHandle = new Microsoft.Win32.SafeHandles.SafeFileHandle(stdHandle, true);
                FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);

                //�����encoding��Ҫ����Ŀ���̨���������õ���ͬ������������롣
                StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
                standardOutput.AutoFlush = true;
                Console.SetOut(standardOutput);
            }
            catch (System.Exception e)
            {
                Debug.Log("Couldn't redirect output: " + e.Message);
                return;
            }

            Console.WriteLine("Console.InputEncoding :" + Console.InputEncoding.CodePage.ToString());
            Console.WriteLine("Console.OutputEncoding :" + Console.OutputEncoding.CodePage.ToString());
            Console.WriteLine("StreamWriter.Encoding :" + encoding.CodePage.ToString());
            Console.WriteLine("UnityConsole ��ʼ����ϡ�");

        }

        public void Shutdown()
        {
            Console.SetOut(oldOutput);
            FreeConsole();
        }

        public void SetTitle(string strName)
        {
            SetConsoleTitleA(strName);
        }

        private const int STD_INPUT_HANDLE = -10;
        private const int STD_OUTPUT_HANDLE = -11;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleTitleA(string lpConsoleTitle);

        //public delegate bool HandlerRoutine(int dwCtrlType);

        //const int CTRL_C_EVENT = 0;
        //const int CTRL_BREAK_EVENT = 1;
        //const int CTRL_CLOSE_EVENT = 2;
        //const int CTRL_LOGOFF_EVENT = 5;
        //const int CTRL_SHUTDOWN_EVENT = 6;
        //[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        //public static extern bool SetConsoleCtrlHandler(HandlerRoutine HandlerRoutine, bool add);
    }
}