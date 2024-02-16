using NetLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//����˵���Ϸҵ������
public class ServerNet : MonoBehaviour
{
    public static ServerNet inst;
    KcpSocketServer kcpserver;
    private void Awake()
    {
        gameObject.AddComponent<ServerMsg>();
#if UNITY_EDITOR
        EditorApplication.quitting += EditorApplication_quitting;
#endif
        inst = this;
        kcpserver = new KcpSocketServer();
        kcpserver.Create(50000);
        kcpserver.OnRecvAction += OnClientRecvSocket;
        kcpserver.OnLog += OnKcpLog;
        //_ = ShowConsoleLog();

        Debug.Log("��ʼ:"+ 50000);
    }

    private void EditorApplication_quitting()
    {
        Clear();
    }

    void Clear()
    {
        Debug.Log("ServerNet����");
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

    private void OnClientRecvSocket(KcpFlag flag, uint _convId, byte[] _buff, int len)
    {
        if (flag == KcpFlag.MSG)
        {
            ServerMsg.inst.Msg(_convId, _buff, len);
        }
    }

    public void SendLog(int t, string txt)
    {
        var e = kcpserver.kcpClientDict.GetEnumerator();
        e.MoveNext();
        for (int i = 0; i < kcpserver.kcpClientDict.Count; e.MoveNext(), i++)
        {
            //Debug.Log("������־���ͻ���");
            //byte[] buff0 = StructConverter.Pack(new object[] { t,txt },true,out var txtsend);
            kcpserver.SendMsg(e.Current.Value, new object[] { t, txt });
        }

    }


}
