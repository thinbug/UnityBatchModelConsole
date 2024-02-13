using NetLibrary;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ServerMsg : MonoBehaviour
{
    public static ServerMsg inst;
    private void Awake()
    {
        inst = this;
    }

    public void Msg(uint _convId, byte[] _buff, int len)
    {
        object[] type_ = StructConverter.Unpack(StructConverter.EndianHead + "i", _buff, 12, 4);
        GameSocketFlag flag = (GameSocketFlag)type_[0];
        switch (flag)
        {
            case GameSocketFlag.GET_USER_DATA:
                break;
        }
    }
}
