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
