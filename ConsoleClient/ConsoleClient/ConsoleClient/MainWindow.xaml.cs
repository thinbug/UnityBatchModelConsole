
using NetLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace ConsoleClient
{
    
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string appName = "服务器控制台";

        List<LogInfo> logList;
        public MainWindow()
        {
            logList = new List<LogInfo>();
            InitializeComponent();
            string ver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;
            this.Title = appName + " - " + ver;

            KcpSocketClient kcpSocketClient = new KcpSocketClient();
            kcpSocketClient.Create("127.0.0.1", 27100);
            kcpSocketClient.OnRecvAction += OnClientRecvSocket;
            kcpSocketClient.OnLog += OnKcpLog;

            _= ShowConsoleLog();
        }

        #region 网络程序Log

        void OnClientRecvSocket(KcpFlag kcpFlag, byte[] _buff, int len)
        {
            Console.WriteLine("client: "  + "收到了:" + kcpFlag);
        }

        

        void OnKcpLog(int _type,string _outstr)
        {
            logList.Add(new LogInfo() { type = _type, log = _outstr });
        }

        async Task ShowConsoleLog()
        {
            while (true)
            {
                await Task.Delay(100);
                while (logList.Count > 0)
                {
                    int type = logList[0].type;
                    string outstr = logList[0].log;
                    logList.RemoveAt(0);
                    LogType lt = (LogType)type;
                    switch (lt)
                    {
                        case LogType.Log:
                            tbConsole.Text += outstr;
                            break;
                        case LogType.Warn:
                            tbConsole.Text += outstr;
                            break;
                        case LogType.Error:
                            tbConsole.Text += outstr;
                            break;
                    }
                }
            }
        }
        #endregion

    }
}
