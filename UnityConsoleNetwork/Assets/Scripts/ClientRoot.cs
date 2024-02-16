
using UnityEngine;

public class ClientRoot : MonoBehaviour
{
    public static ClientRoot inst;

    public string userId;
    public string userPwd;
    private void Awake()
    {
        inst = this;
    }
    
    void Start()
    {
        gameObject.AddComponent<PlayerSocket>();
        UIRoot.inst.OpenUI();
    }

    public void LoginServer(string ip, int port,string _userId, string _userPwd)
    {
        userId = _userId;
        userPwd = _userPwd;
        PlayerSocket.inst.Login(ip, port);
        PlayerSocket.inst.kcp.OnConnetOK += OnConnetOK;
        PlayerSocket.inst.kcp.OnConnetClose += OnConnetClose;
    }

    private void OnConnetOK()
    {
        //连接成功后进行数据请求
        Debug.Log("OnConnetOK");
        UserLogin();
    }
    private void OnConnetClose()
    {
        Debug.Log("OnConnetClose");
    }

    private void UserLogin()
    {
        //用户默认用户名在本地就是local
        PlayerSocket.inst.ToServer(GameSocketFlag.GET_USER_DATA, userId+","+userPwd);
    }

}
