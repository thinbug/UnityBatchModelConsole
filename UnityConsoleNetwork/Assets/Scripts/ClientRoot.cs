using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientRoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PlayerSocket>();
        PlayerSocket.inst.kcp.OnConnetOK += OnConnetOK;
    }

    private void OnConnetOK()
    {
        //连接成功后进行数据请求
        GetUserData();
    }

    private void GetUserData()
    {
        //用户默认用户名在本地就是local
        PlayerSocket.inst.ToServer(GameSocketFlag.GET_USER_DATA,"local");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
