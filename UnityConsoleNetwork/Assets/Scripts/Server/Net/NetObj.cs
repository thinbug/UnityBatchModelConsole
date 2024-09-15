using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��Ҫͬ�����������
public class NetObj : MonoBehaviour
{
    public bool needUpdateGrid;  //��ǰ��������Ƿ���Ҫ����grid�仯,�������ƶ����߱�����,���߱�����
    public Transform objTransform;  //���������ȡ
    public int netGrid = -1;    //��ǰ����������ͬ������

    public float netUpdateGridRate = 1f;    //ͬ��������Ƶ��,����1,��ô1��һ�μ����Ƿ�����仯,ͨ�������ƶ��ٶ�������
    private void Awake()
    {
        //nextGridProcessTime = Time.time + Random.value * 3f;   //����ͬ����������
    }

    float nextGridProcessTime;  //��һ�μ����ʱ��
    Vector3 lastTransformPos;   //��һ�ζ����λ��
    //������㵱ǰ��ͬ������,���ƶ�����Ҫ�����Ƿ�ı���ͬ������
    public void ProcessGridMove()
    {
        float ntime = Time.time;
        if (ntime < nextGridProcessTime) return;
        nextGridProcessTime = ntime + netUpdateGridRate;

        if (Mathf.Abs(objTransform.position.x - lastTransformPos.x) < NetMapManager.inst.minDisGrid) return;
        if (Mathf.Abs(objTransform.position.z - lastTransformPos.z) < NetMapManager.inst.minDisGrid) return;
    }

    //���뿪ͬ���ռ�,������߱����ʰȡ
    public void LeaveSpace()
    { 
    
    }

    //����ͬ���ռ�
    public void JoinSpace()
    { 
    
    }
}
