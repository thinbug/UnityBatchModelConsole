using NetLibrary;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ServerMsg : MonoBehaviour
{
    public static ServerMsg inst;

    Dictionary<string, uint> Users;
    private void Awake()
    {
        inst = this;
        Users = new Dictionary<string, uint>();
    }

    public void Msg(uint _convId, byte[] _buff, int len)
    {
        object[] type_ = StructConverter.Unpack(StructConverter.EndianHead + "i", _buff, 12, 4);
        GameSocketFlag flag = (GameSocketFlag)type_[0];
        switch (flag)
        {
            case GameSocketFlag.GET_USER_DATA:
                GetUserData(_convId, _buff, len);
                break;
        }
    }

    //玩家登陆,用户和密码用逗号分割
    void GetUserData(uint _convId, byte[] _buff, int len)
    {
        string sOut = StructConverter.UnPackString(true, _buff, 16, len - 16);
        UserManager.inst.UserLogin(sOut);
    }
}
