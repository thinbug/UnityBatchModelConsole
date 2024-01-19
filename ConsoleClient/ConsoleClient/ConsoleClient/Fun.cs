using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;
/*
 // Start is called before the first frame update
    void Start()
    {
        string str1 = $"<color=#FF0000>用例1: </color>第1个提示。";
        string str2 = $"<color=#FFFF00>用例2: </color>第2个提示。<color=#FFFF00>用例2: </color>这是多个color文本。";
        string str3 = $"<color=#00FF00>用例3: <color=#FF00FF>包含嵌套子集颜色提示</color>的测试。</color>";

        Debug.Log(str1);
        Debug.Log(str2);
        Debug.Log(str3);
    }
 */
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
        public class LogComputFormat
        {
            public int lv;
            public string color;
            public string txt;
        }
        public static string LogFormat(string source)
        {
            
            string bStr = "<color";     //头
            string eStr = "</color>";   //尾
            string sTemp = source;
            //List<string> list = new List<string>();
            int nowStartIndex = 0;
            int nowEndIndex = 0;
            int level = 0;
            Regex regB = new Regex("\\<color=(?<color>.+?)\\>", RegexOptions.IgnoreCase);
            MatchCollection matchesB = regB.Matches(source);
            if (matchesB.Count <= 1)
                return source;
            Regex regE = new Regex("\\</color\\>", RegexOptions.IgnoreCase);
            MatchCollection matchesE = regE.Matches(source);
            if(matchesE.Count != matchesB.Count)
                return source;

            string s2 = source;
            Match lastmB;

            List<LogComputFormat> list = new List<LogComputFormat>();
            int nowlevel = 0;
            LogComputFormat lv;
            
            for (int i = 0; i < matchesB.Count; i++)
            {
                Match mB = matchesB[i];
                if (mB.Index == 0)
                {
                    lv = new LogComputFormat() { lv = 0, txt = "", color = "" };
                    list.Add(lv);
                }
                else
                {
                    lv = new LogComputFormat() { lv = 0, txt = source.Substring(0,mB.Index), color = "" };
                    list.Add(lv);
                    
                }
                
                for (int j = 0; j < i; j++)
                {
                    //找结尾
                    Match mE = matchesB[i];
                    if (mE.Index < mB.Index)
                    {
                        //说明有结尾,不是嵌套的
                        break;
                    }
                    else
                    {
                        //如果嵌套了,给前面插入结构,后面插入头
                        string color = lastmB.Groups["color"].Value;
                        s2 = s2.Substring(0, mE.Index) + color + s2.Substring(mE.Index);
                    }
                }
            }

            while (true)
            {
                int index_b = sTemp.IndexOf(bStr, nowStartIndex);
                if (index_b == -1)
                    break;


                //如果找到,那么看后面是否还跟着一个头
                int index_e = sTemp.IndexOf(eStr, index_b + bStr.Length);
                if (index_e == -1)
                    break;

                //如果找到了,说明中间有插,那么需要给这个头的头部插入尾部,给尾部插入头部
                Match mat = reg.Match(source, nowStartIndex+ bStr.Length);
                if (mat.Length > 0)
                {
                    //找到了
                    source = source.Substring()
                }
            }
            //Regex reg = new Regex("\\<color=#.*?\\>((?!color).)*?\\</color\\>", RegexOptions.IgnoreCase);
            //MatchCollection matches = reg.Matches(source);
            return "";
        }

        public static void LogOutputColor(FlowDocument fd,string source)
         {
            //正则1：只匹配整个
            //Regex reg = new Regex("\\<color=#.*?\\>.*?\\</color\\>", RegexOptions.IgnoreCase);
            //正则2：匹配Group
            Regex reg = new Regex("\\<color=(?<color>.+?)\\>(?<txt>.*?)\\</color\\>", RegexOptions.IgnoreCase);

            

            BrushConverter brushConverter = new BrushConverter();
            Paragraph p = new Paragraph();


            MatchCollection matches = reg.Matches(source);

            if (matches.Count == 0)
            {
                //如果没有任何匹配，直接添加显示。
                Run run = new Run();
                run.Text = source;
                p.Inlines.Add(run);
            }

            int nowIndex = 0;   //用来计算匹配位置是否有其他字符串
            for (int i = 0; i < matches.Count; i++)
            {
                Match match = matches[i];

                if (match.Index > nowIndex)
                {
                    //说明这里有其他字符
                    Run runInsert = new Run();
                    runInsert.Text = source.Substring(nowIndex, match.Index-nowIndex);
                    p.Inlines.Add(runInsert);
                }
                nowIndex = match.Index+match.Length;

                string color = match.Groups["color"].Value;
                string txt = match.Groups["txt"].Value;


                Run run = new Run(txt);
                run.Foreground = (Brush)brushConverter.ConvertFromString(color);
                run.Text = txt;
                p.Inlines.Add(run);
            }
            if (nowIndex < source.Length)
            {
                //说明末尾还有字符串
                Run runInsert = new Run();
                runInsert.Text = source.Substring(nowIndex);
                p.Inlines.Add(runInsert);
            }

            fd.Blocks.Add(p);
        }

        //public static void LogOutputColor2()
    }
}
