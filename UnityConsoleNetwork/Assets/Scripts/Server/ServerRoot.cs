using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsConsoleMine;

public class ServerRoot : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<ConsoleMain>(); //控制台监听程序
        gameObject.AddComponent<ServerNet>();//玩家网络同步
        gameObject.AddComponent<DBMain>();  //玩家本地数据
        gameObject.AddComponent<UserManager>();

        gameObject.AddComponent<NetMapManager>();//地图网络同步管理器
    }

}
