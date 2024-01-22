
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 用来显示测试按钮的.
/// </summary>
public class ButtonGUI : MonoBehaviour
{
    public bool showButton = true;

   
    bool inited = false;
    List<GUIStyle> btnStyles = new List<GUIStyle>();
    GUIStyle defStyle;
    List<Color> colors = new List<Color>();
    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }
    private void Start()
    {
        StartCoroutine(xx());
    }

    IEnumerator xx()
    {
        yield return new WaitForSeconds(3f);
        InitStyle();
        
    }

    public void InitStyle()
    {
        if (defStyle == null || defStyle.normal.background == null)
        {
            defStyle = new GUIStyle();
            Color c = Color.yellow;
            defStyle.normal.background = MakeTex(600, 1, c);
            defStyle.alignment = TextAnchor.MiddleCenter;
            defStyle.normal.textColor = new Color(1f, 0f, 0f);   //设置字体颜色
            defStyle.fontSize = 30;       //字体大小

            inited = true;
        }
    }


    void OnGUI()
    {
        
        if (!inited)
            return;
        //showButton = false;
        if (!showButton)
            return;

        InitStyle();
       
        int x = 100;
        int y = 50;
        if (GUI.Button(new Rect(x + 1, y, 120, 100), "提示1", defStyle))
        {
            
            Debug.Log("提示1");
            
            return;
        }

        if (GUI.Button(new Rect(x + 150, y, 120, 100), "提示2", defStyle))
        {
            
            Debug.Log("提示2");
            
            return;
        }
        if (GUI.Button(new Rect(x + 300, y, 120, 100), "提示3", defStyle))
        {
            Debug.Log("提示3");
            return;
        }
       
    }
}
