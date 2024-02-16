using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Íæ¼Ò¹ÜÀíÆ÷
public class UserManager : MonoBehaviour
{
    public static UserManager inst;
    private void Awake()
    {
        inst = this;
    }
    public void UserLogin(string parm)
    {
        Debug.Log("Server - GetUserData;" + parm);
        string[] userparm = parm.Split(',');
        bool login = DBMain.inst.Login(userparm[0], userparm[1]);
        Debug.Log("dblogin:"+login);
    }
}
