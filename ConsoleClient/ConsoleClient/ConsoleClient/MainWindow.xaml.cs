
using NetLibrary;
using System;
using System.Collections.Generic;
using System.Data;
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
            kcpSocketClient.Create("192.168.3.76", 27100);
            kcpSocketClient.OnRecvAction += OnClientRecvSocket;
            kcpSocketClient.OnLog += OnKcpLog;
            _= ShowConsoleLog();
            
            //logList.Add(new LogInfo() { type = 1, log = "<color=#6780AB>找不到缓存资源,jvgPictureLoader下载CDN资源:</color>" });
        }

        #region 网络程序Log

        void OnClientRecvSocket(KcpFlag kcpFlag, byte[] _buff, int len)
        {
            Console.WriteLine("client: "  + "收到了:" + kcpFlag);
            if (kcpFlag == KcpFlag.MSG)
            {
                int lt = BitConverter.ToInt32(_buff, 12);
                string msg = Encoding.UTF8.GetString( _buff, 16, len - 16);
                //string msg = BitConverter.ToString(_buff, 16, len- 16);
                
                logList.Add(new LogInfo() { type = lt, log = msg });

            }
            //object[] parm = StructConverter.Unpack("<is", _buff, 12, len - 12);
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
                    LogType  type = (LogType)logList[0].type;
                    string outstr = logList[0].log;
                    logList.RemoveAt(0);

                    Fun.LogOutputColor(tbConsole, type, outstr);
                    //LogType lt = (LogType)type;

                    //Paragraph p = new Paragraph();  // Paragraph 类似于 html 的 P 标签
                    //Run r = new Run(outstr);      // Run 是一个 Inline 的标签
                    //p.Inlines.Add(r);
                    //tbConsole.Blocks.Add(p);

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
            string str2 = $"123<color=#FFFF00>用例2: </color>第2个提示。<color=#FF0000>用例2: </color>这是多个color文本。";
            //string str3 = $"<color=#00FF00>用例3: <color=#FF00FF>包含嵌套子集颜色提示</color>的测试。</color>";
            //str3 = "<div>  <h3>这是一个在 div 元素中的标题。</h3></div>";
            
            Fun.LogOutputColor(tbConsole,LogType.Log, str2);
            
            //Fun.LogOutputColor(tbConsole, str2);
            //Fun.LogOutputColor(tbConsole, str3);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ServerLink slWin = new ServerLink();
            slWin.ShowDialog();
        }
    }
}
