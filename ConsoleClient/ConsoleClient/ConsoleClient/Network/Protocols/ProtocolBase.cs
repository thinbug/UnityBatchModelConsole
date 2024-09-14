using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleClient.Network
{
    //网络协议基类
    //把结构体转成二进制的基类
    public class ProtocolBase
    {

        protected List<FieldInfo> fields;  //数据结构类的字段

        public ProtocolBase() {
            //把数据的字段进行排序,方便后面按照数据进行排序转换成二进制,目前使用的MetadataToken
            FieldInfo[] fieldInfos = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            fields = fieldInfos.OrderBy(f => f.MetadataToken).ToList();
            //fields = fieldInfos.OrderBy(f => f.Name).ToList();
            for (int i = 0; i < fields.Count; i++)
            {
                FieldInfo field = fields[i];
                if (field.GetValue(this) == null)
                {
                    Console.WriteLine(fields[i].Name+"的值为空.");
                }
                //Console.WriteLine(fields[i].Name);
            }
        }

        //把一个对象转成二进制
        public static byte[] ConvertToBytes(int protocolNo,ProtocolBase data)
        {
            List<byte> outputBytes = new List<byte>();

            //以小端来传输数据
            bool endianFlip = !BitConverter.IsLittleEndian;

            //把这个协议编号写入开头
            byte[] noBytes = BitConverter.GetBytes(protocolNo);
            if (endianFlip == true) Array.Reverse(noBytes);
            outputBytes.AddRange(noBytes);

            for (int i = 0; i < data.fields.Count; i++)
            { 
                FieldInfo field = data.fields[i];
                object val = field.GetValue(data);
                
                byte[] theseBytes = TypeUnitGetBytes(val,out int variableSize);
                if (endianFlip == true) Array.Reverse(theseBytes);

                if (theseBytes == null)
                {
                    Console.WriteLine($"无法找到这个类型{field.Name},或者值是null.");
                    return new byte[0];
                }
                //如果是可变长度,先把长度写进去
                if (variableSize > 0)
                {
                    byte[] sizeBytes = BitConverter.GetBytes(variableSize);
                    if (endianFlip == true) Array.Reverse(sizeBytes);
                    outputBytes.AddRange(sizeBytes);
                }
                //把数据写入
                outputBytes.AddRange(theseBytes);


            }

            return outputBytes.ToArray();
        }

        
        

        //把一个二进制数据给到一个数据结构
        public static void ConvertToObject(byte[] netBytes,int bytePosition, ProtocolBase data)
        {
            bool endianFlip = !BitConverter.IsLittleEndian;

            //bytePosition = 4;  //从4开始解析,第一个int是协议编号
            for (int i = 0; i < data.fields.Count; i++)
            {
                FieldInfo field = data.fields[i];
                object val = field.GetValue(data);

                //int sz = TypeUnitGetSize(val, out bool variableVar);
                object valNow = TypeUnitValue(val, netBytes, bytePosition, endianFlip, out int valueSize);
                if (valueSize > 0)
                {
                    field.SetValue(data, valNow);
                    bytePosition += valueSize;
                }
            }
            //Console.WriteLine("对象转换完成");
        }



        //传入一个类型,返回二进制
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">传入的数据类型</param>
        /// <param name="variableSize">表示可变长度,如果是int等固定的返回0,可变的返回具体大小</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static byte[] TypeUnitGetBytes(object o, out int variableSize)
        {
            variableSize = -1;
            if (o is bool) return BitConverter.GetBytes((bool)o);
            if (o is int) return BitConverter.GetBytes((int)o);
            if (o is float) return BitConverter.GetBytes((float)o);
            if (o is uint) return BitConverter.GetBytes((uint)o);
            if (o is long) return BitConverter.GetBytes((long)o);
            if (o is ulong) return BitConverter.GetBytes((ulong)o);
            if (o is short) return BitConverter.GetBytes((short)o);
            if (o is ushort) return BitConverter.GetBytes((ushort)o);
            if (o is string)
            {
                byte[] tmp = System.Text.Encoding.UTF8.GetBytes((string)o);
                variableSize = tmp.Length;
                return tmp;
            }
            if (o is byte || o is sbyte) return new byte[] { (byte)o };
            if (o is Vector3)
            {
                byte[] tmp = new byte[12];
                Array.Copy(BitConverter.GetBytes(((Vector3)o).X), 0, tmp, 0, 4);
                Array.Copy(BitConverter.GetBytes(((Vector3)o).Y), 0, tmp, 4, 4);
                Array.Copy(BitConverter.GetBytes(((Vector3)o).Z), 0, tmp, 8, 4);
                return tmp;
            }

            Console.WriteLine("TypeUnitGetBytes - Unsupported object type found");
            return null;
            //Console.WriteLine("TypeUnitGetBytes - Unsupported object type found");
            //hrow new ArgumentException("TypeUnitGetBytes - Unsupported object type found");
        }

        //传入一个类型,返回类型长度
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o">传入的数据类型</param>
        /// <param name="variableSize">表示可变长度,如果是int等固定的返回0,可变的返回具体大小</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static object TypeUnitValue(object o, byte[] netBytes, int bytePosition, bool endianFlip,  out int valueSize)
        {

            valueSize = 0;
            if (o is bool)
            {
                valueSize = 1;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize); 
                return (object)(bool)BitConverter.ToBoolean(netBytes, bytePosition);
            }
            if (o is int)
            {
                valueSize = 4;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize);
                return (object)(int)BitConverter.ToInt32(netBytes, bytePosition);
            }
            if (o is float)
            {
                valueSize = 4;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize);
                return (object)(float)BitConverter.ToSingle(netBytes, bytePosition);
            }
            if (o is uint)
            {
                valueSize = 4;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize);
                return (object)(uint)BitConverter.ToUInt32(netBytes, bytePosition);
            }
            if (o is long)
            {
                valueSize = 8;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize);
                return (object)(long)BitConverter.ToInt64(netBytes, bytePosition);
            }
            if (o is ulong)
            {
                valueSize = 8;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize);
                return (object)(ulong)BitConverter.ToUInt64(netBytes, bytePosition);
            }
            if (o is short)
            {
                valueSize = 2;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize);
                return (object)(short)BitConverter.ToInt16(netBytes, bytePosition);
            }
            if (o is ushort)
            {
                valueSize = 2;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize);
                return (object)(ushort)BitConverter.ToUInt16(netBytes, bytePosition);
            }
            if (o is string)
            {
                //首先获取到长度,把长度拿到
                int headsize = 4; //字符串的头部信息是int,所以是4个字节
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, headsize);
                int stringLen = (int)BitConverter.ToInt32(netBytes, bytePosition);
                valueSize = headsize + stringLen;
                string outstr = System.Text.Encoding.UTF8.GetString(netBytes, bytePosition + headsize, stringLen);

                return outstr;
            }
            
            if (o is byte || o is sbyte)
            {
                valueSize = 1;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition, valueSize);
                byte[] buf = new byte[1];
                Array.Copy(netBytes, bytePosition, buf, 0, 1);
                return (object)(byte)buf[0];
            }
            if (o is Vector3)
            {
                valueSize = 12;
                Vector3 outv3 = new Vector3();
                int onesize = 4;
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition + 0 * onesize, onesize);
                outv3.X = (float)BitConverter.ToSingle(netBytes, bytePosition + 0 * onesize);
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition + 1 * onesize, onesize);
                outv3.Y = (float)BitConverter.ToSingle(netBytes, bytePosition + 1 * onesize);
                if (endianFlip == true) Array.Reverse(netBytes, bytePosition + 2 * onesize, onesize);
                outv3.Z = (float)BitConverter.ToSingle(netBytes, bytePosition + 2 * onesize);
                return outv3;
            }

            Console.WriteLine("TypeUnitValue - Unsupported object type found");
            throw new ArgumentException("TypeUnitValue - Unsupported object type found");
        }
    }
}
