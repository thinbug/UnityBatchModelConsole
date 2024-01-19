
using NetLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;


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
            
            logList.Add(new LogInfo() { type = 1, log = "<color=#6780AB>找不到缓存资源,jvgPictureLoader下载CDN资源:</color>" });
            logList.Add(new LogInfo() { type = 1, log = "<Span Foreground=\"#FF0000\">not expect</Span>" });
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

                    Paragraph p = new Paragraph();  // Paragraph 类似于 html 的 P 标签
                    Run r = new Run(outstr);      // Run 是一个 Inline 的标签
                    p.Inlines.Add(r);
                    tbConsole.Blocks.Add(p);

                    //switch (lt)
                    //{
                    //    case LogType.Log:
                    //        tbConsole. .Text += outstr;
                    //        break;
                    //    case LogType.Warn:
                    //        tbConsole.Text += outstr;
                    //        break;
                    //    case LogType.Error:
                    //        tbConsole.Text += outstr;
                    //        break;
                    //}
                }
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str0 = $"<color=#FF0000>用例1: </color>";
            string str1 = $"<color=#FF0000>用例1: </color>第1个提示。";
            string str2 = $"<color=#FFFF00>用例2: </color>第2个提示。<color=#FFFF00>用例2: </color>这是多个color文本。";
            string str3 = $"<color=#00FF00>用例3: <color=#FF00FF>包含嵌套子集颜色提示</color>的测试。</color>";
            Fun.LogFormat(str3);
            Fun.LogOutputColor(tbConsole, str3);
            
            //Fun.LogOutputColor(tbConsole, str2);
            //Fun.LogOutputColor(tbConsole, str3);
        }
    }
}
