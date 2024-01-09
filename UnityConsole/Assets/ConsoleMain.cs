
using System;
using System.Threading.Tasks;
using UnityEngine;


namespace WindowsConsoleMine
{
    public class ConsoleMain : MonoBehaviour
    {
        private void Awake()
        {
        }
        #if !UNITY_EDITOR && UNITY_STANDALONE
        ConsoleWindow consoleWindow;

        void Start()
        {
            gameObject.AddComponent<ConsoleDebugLogCallback>();

            consoleWindow = new ConsoleWindow();

            Debug.Log("Log 你好!");
            Debug.LogWarning("LogWarning 你好!");
            Debug.LogError("LogError 你好!");
        }
        void OnApplicationQuit()
        {
            Debug.Log("开始关闭!");
            consoleWindow.Shutdown();
            Debug.Log("开始关闭!");

        }


        
        #endif
    }
}