using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientRoot : MonoBehaviour
{
    public static ClientRoot inst;
    private void Awake()
    {
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PlayerSocket>();
        //PlayerSocket.inst.kcp.OnConnetOK += OnConnetOK;
        UIRoot.inst.OpenUI();
    }

    public void LoginServer(string ip, int port)
    {
        PlayerSocket.inst.Login(ip, port);
    }

    private void OnConnetOK()
    {
        //���ӳɹ��������������
        GetUserData();
    }

    private void GetUserData()
    {
        //�û�Ĭ���û����ڱ��ؾ���local
        PlayerSocket.inst.ToServer(GameSocketFlag.GET_USER_DATA,"local");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
