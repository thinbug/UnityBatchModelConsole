using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Network
{
    /*
     * 玩家网络协议 - 都是继承于ProtocolBase
     */

    //玩家移动
    public class ProtocolPlayerMove : ProtocolBase
    {
        public Vector3 at = new Vector3(111,222,333);
        public int a = 1;
        public long b = 2;
        public float z = 3f;
        public string c = "123";
    }

    //玩家飞行
    public class ProtocolPlayerFly : ProtocolBase
    {
        public int a = 1;
    }
}
