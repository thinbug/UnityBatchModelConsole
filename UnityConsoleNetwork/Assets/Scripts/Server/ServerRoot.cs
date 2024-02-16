using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsConsoleMine;

public class ServerRoot : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<ConsoleMain>();
        gameObject.AddComponent<ServerNet>();
        gameObject.AddComponent<DBMain>();
        gameObject.AddComponent<UserManager>();
    }

}
