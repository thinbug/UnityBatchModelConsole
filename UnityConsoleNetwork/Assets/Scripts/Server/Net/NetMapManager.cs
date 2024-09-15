using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ϸ����ͬ��������
public class NetMapManager : MonoBehaviour
{
    //����ͬ���ǻ��������,��9�������ڵĽ���ͬ��

    public static NetMapManager inst;

    //�����ͼ��С
    public Vector2Int mapSize = new Vector2Int(2048, 2048);
    
    //�зֵĿ���
    //���Ϊ10,��ͼ�з�Ϊ10x10��������ͬ������.�����ͼ��2000,ÿ�����Ӿ���100��,����ͬ���������300x300��С
    public int gridSplit = 20;

    public float minDisGrid = 5f;   //������Զ����ż����Ƿ����ͬ������,ҪС��ÿ��ͬ������İ뾶

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
