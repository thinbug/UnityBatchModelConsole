using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ConsoleClient
{
    /// <summary>
    /// ServerLink.xaml 的交互逻辑
    /// </summary>
    public partial class ServerLink : Window
    {
        public class ServerData
        {
            public string name;
            public long time;
        }
        List<ServerData> serversList = new List<ServerData>();
        public ServerLink()
        {
            InitializeComponent();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            this.Top = mainWindow.Top + (mainWindow.Height - Height) / 2;
            this.Left = mainWindow.Left + (mainWindow.Width - Width) / 2;
            Title = "连接服务器";
            ReadList();
        }

        void ReadList()
        {
            serversList = new List<ServerData>();
            lbServers.Items.Clear();
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey
        ("Software\\mmcConsole\\serverlist", true);
            //判断键是否存在
            if (key != null)
            {
                //检索包含此项关联的所有值名称 即url1 url2 url3
                string[] names = key.GetValueNames();
                foreach (string str in names)
                {
                    //获取url中相关联的值
                    long v = long.Parse(key.GetValue(str).ToString());
                    serversList.Add(new ServerData { name = str , time = v });
                    
                    //lbServers.Items.Add(key.GetValue(str).ToString());
                }
                //排序
                List<ServerData> sortedServerData = serversList.OrderByDescending(s => s.time).ToList();
                serversList = sortedServerData;
                foreach (var d in sortedServerData)
                {
                    //获取url中相关联的值
                    lbServers.Items.Add(d.name);
                    //lbServers.Items.Add(key.GetValue(str).ToString());
                }
                
            }
        }

        void SaveList()
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey
            ("Software\\mmcConsole", true);
            //判断键是否存在
            if (key == null)
            {
                key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("Software\\mmcConsole", true);
            }
            else
            {
                key.DeleteSubKey("serverlist");
            }

            //检索包含此项关联的所有值名称 即url1 url2 url3
            key = key.CreateSubKey("serverlist", true);
            foreach (var ser in serversList)
            {
                //获取url中相关联的值
                key.SetValue(ser.name, ser.time.ToString());
            }
            ReadList();
        }

        private void btnLink_Click(object sender, RoutedEventArgs e)
        {
            string savestr = tbIp.Text + "|" + tbPort.Text;
            int idx = lbServers.Items.IndexOf(savestr);
            if (idx != -1)
            {
                lbServers.Items.RemoveAt(idx);
            }

            serversList.Add(new ServerData { name = savestr , time = DateTimeOffset.Now.ToUnixTimeSeconds() });
            
            SaveList();
        }

        private void lbServers_Selected(object sender, RoutedEventArgs e)
        {
            if (lbServers.SelectedIndex < 0)
                return;
            string str = lbServers.Items[lbServers.SelectedIndex].ToString();
            string[] parm = str.Split('|');
            if (parm.Length != 2)
                return;
            tbIp.Text = parm[0];
            tbPort.Text = parm[1];
        }

        void DelServer(int idx)
        {
            serversList.RemoveAt(idx);
            SaveList();
        }
        void DelAllServer()
        {
            serversList.Clear();
            SaveList();
        }

        #region 规则添加编辑等操作
        //右键菜单点击
        private void OnSvrMenuClick(object sender, RoutedEventArgs e)
        {
            string menuname = ((MenuItem)sender).Name;
            int index = lbServers.SelectedIndex;

            switch (menuname)
            {
                case "MenuItemSvrDel":
                    if (index == -1)
                        return;
                    DelServer(index);
                    break;
                case "MenuItemSvrDelAll":
                    if (index == -1)
                        return;
                    DelAllServer();
                    break;
            }
        }
        #endregion
    }
}
