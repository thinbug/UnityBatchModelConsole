using NetLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMain : MonoBehaviour
{
    public static ServerMain inst;
    KcpSocketServer kcpserver;
    private void Awake()
    {
        inst = this;
        kcpserver = new KcpSocketServer();
        kcpserver.Create(27100);
        kcpserver.OnRecvAction += OnClientRecvSocket;
        kcpserver.OnLog += OnKcpLog;
        //_ = ShowConsoleLog();

        Debug.Log("开始");
    }
  

    private void OnKcpLog(int arg1, string arg2)
    {
        Debug.Log(arg2);
    }

    private void OnClientRecvSocket(KcpFlag flat, uint _convId, byte[] _buff, int len)
    {
        
    }

    public void SendLog(int t, string txt)
    {
        var e = kcpserver.kcpClientDict.GetEnumerator();
        e.MoveNext();
        for (int i = 0; i < kcpserver.kcpClientDict.Count; e.MoveNext(), i++)
        {
            //Debug.Log("发送日志到客户端");
            //byte[] buff0 = StructConverter.Pack(new object[] { t,txt },true,out var txtsend);
            kcpserver.SendMsg(e.Current.Value, new object[] { t, txt });
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
