
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

            Debug.Log("Log ���!");
            Debug.LogWarning("LogWarning ���!");
            Debug.LogError("LogError ���!");
        }
        void OnApplicationQuit()
        {
            Debug.Log("��ʼ�ر�!");
            consoleWindow.Shutdown();
            Debug.Log("��ʼ�ر�!");

        }


        
        #endif
    }
}