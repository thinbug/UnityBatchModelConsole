using NetLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ConsoleClient.Network
{
    public class ProtocolClient
    {
        

        //所有网络协议的队列，key和value ，服务端和客户端必须对应起来
        Dictionary<int,ProtocolBase> protocolDict = new Dictionary<int, ProtocolBase>
        {
            { 1001 , new ProtocolPlayerMove() },
            { 1002 , new ProtocolPlayerFly() },
        };
      


        
        /// <summary>
        /// 处理网络协议
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="bytePosition"></param>
        /// <param name="protocolNo"></param>
        public void ProtocolProcess(byte[] bytes, int bytePosition ,int protocolNo)
        {
            if (!protocolDict.ContainsKey(protocolNo))
            {
                Console.WriteLine("没有这个网络协议。");
                return;
            }
            ProtocolBase pr = protocolDict[protocolNo];
            ProtocolBase.ConvertToObject(bytes, bytePosition, pr);

            Console.WriteLine($"接收到网络协议{protocolNo}数据。");
        }
    }
}
