
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;

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
        

        public static void LogOutputColor(FlowDocument fd,LogType lt,string source)
        {
            //正则1：只匹配整个
            //Regex reg = new Regex("\\<color=#.*?\\>.*?\\</color\\>", RegexOptions.IgnoreCase);
            //正则2：匹配Group , 不支持嵌套颜色输出
            Regex reg = new Regex("\\<color=(?<color>.+?)\\>(?<txt>.*?)\\</color\\>", RegexOptions.IgnoreCase);

            BrushConverter brushConverter = new BrushConverter();
            Paragraph p = new Paragraph { LineHeight=0.1 };//keepwithnext是没空行
            

            MatchCollection matches = reg.Matches(source);

            if (matches.Count == 0)
            {
                //如果没有任何匹配，直接添加显示。
                Run run = new Run();
                run.Text = source;
                p.Inlines.Add(run);
            }
            else
            {
                int nowIndex = 0;   //用来计算匹配位置是否有其他字符串
                for (int i = 0; i < matches.Count; i++)
                {
                    Match match = matches[i];

                    if (match.Index > nowIndex)
                    {
                        //说明这里有其他字符
                        Run runInsert = new Run();
                        runInsert.Text = source.Substring(nowIndex, match.Index - nowIndex);
                        p.Inlines.Add(runInsert);
                    }
                    nowIndex = match.Index + match.Length;

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
            }

            fd.Blocks.Add(p);
        }

    }
}
