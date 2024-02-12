using NetLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConsoleMain : MonoBehaviour
{
    public static ConsoleMain inst;
    KcpSocketServer kcpserver;
    private void Awake()
    {
#if UNITY_EDITOR
        EditorApplication.quitting += EditorApplication_quitting;
#endif
        inst = this;
        kcpserver = new KcpSocketServer();
        kcpserver.Create(27100);
        kcpserver.OnRecvAction += OnClientRecvSocket;
        kcpserver.OnLog += OnKcpLog;
        //_ = ShowConsoleLog();

        Debug.Log("开始:"+ 27100);
    }

    private void EditorApplication_quitting()
    {
        Clear();
    }

    void Clear()
    {
        Debug.Log("结束");
        if (kcpserver != null)
            kcpserver.Close();
    }

    
    private void OnApplicationQuit()
    {
        Clear();
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


}
