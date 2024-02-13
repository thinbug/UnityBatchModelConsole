using NetLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//客户端用户网络脚本
public class PlayerSocket : MonoBehaviour
{
    public static PlayerSocket inst;
    public KcpSocketClient kcp;


    public bool logined;

    private void Awake()
    {
        inst = this;
    }
    private void Start()
    {
        Login();
    }

    public void Login()
    {
#if UNITY_EDITOR
        EditorApplication.quitting += EditorApplication_quitting;
#endif
        kcp = new KcpSocketClient();
        kcp.Create("127.0.0.1", 50000);
        kcp.OnRecvAction += OnClientRecvSocket;
        kcp.OnLog += OnKcpLog;

        kcp.OnConnetOK += OnConnetOK;
        kcp.OnConnetClose += OnConnetClose;
        //_ = ShowConsoleLog();

        Debug.Log("开始PlayerLogin:" + 50000);
    }

    private void OnConnetClose()
    {
        logined = false;
    }

    private void OnConnetOK()
    {
        logined = true;
    }

    private void EditorApplication_quitting()
    {
        Clear();
    }

    void Clear()
    {
        Debug.Log("结束PlayerLogin");
        if (kcp != null)
            kcp.Close();
    }


    private void OnApplicationQuit()
    {
        Clear();
    }



    private void OnKcpLog(int arg1, string arg2)
    {
        Debug.Log(arg2);
    }

    private void OnClientRecvSocket(KcpFlag flat, byte[] _buff, int len)
    {

    }

    public void ToServer(GameSocketFlag flag,params object[] p)
    {
        kcp.ToServer(flag, p);
    }


}
