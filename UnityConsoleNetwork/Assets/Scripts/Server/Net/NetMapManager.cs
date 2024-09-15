using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏网络同步管理器
public class NetMapManager : MonoBehaviour
{
    //网络同步是划分网格的,以9宫格以内的进行同步

    public static NetMapManager inst;

    //世界地图大小
    public Vector2Int mapSize = new Vector2Int(2048, 2048);
    
    //切分的块数
    //如果为10,地图切分为10x10个正方形同步区域.如果地图是2000,每个格子就是100米,整个同步区域就是300x300大小
    public int gridSplit = 20;

    public float minDisGrid = 5f;   //超过多远距离才计算是否更换同步区域,要小于每个同步区域的半径

    public List<NetObj>[] objList;//= new List<NetObj>[400];

    private void Awake()
    {
        inst = this;
        int allsize = gridSplit * gridSplit;
        objList = new List<NetObj>[allsize];
        for (int i = 0; i < allsize; i++)
        {
            objList[i] = new List<NetObj>();
        }
    }

    public void JoinNet(NetObj netobj)
    {
        //objList.Add(netobj);
    }

    public void LeaveNet(NetObj netobj)
    { 
        //objList.Remove(netobj);
    }

    private void Update()
    {
        
    }

}
