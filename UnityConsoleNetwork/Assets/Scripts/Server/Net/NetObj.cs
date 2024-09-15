using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//需要同步的网络对象
public class NetObj : MonoBehaviour
{
    public bool needUpdateGrid;  //当前网络对象是否需要计算grid变化,当对象移动或者被带走,或者被放置
    public Transform objTransform;  //对象坐标获取
    public int netGrid = -1;    //当前所属的网络同步区域

    public float netUpdateGridRate = 1f;    //同步区域变更频率,如是1,那么1秒一次计算是否区域变化,通常根据移动速度来决定
    private void Awake()
    {
        //nextGridProcessTime = Time.time + Random.value * 3f;   //打乱同步计算周期
    }

    float nextGridProcessTime;  //下一次计算的时间
    Vector3 lastTransformPos;   //上一次对象的位置
    //处理计算当前的同步区域,当移动后需要计算是否改变了同步区域
    public void ProcessGridMove()
    {
        float ntime = Time.time;
        if (ntime < nextGridProcessTime) return;
        nextGridProcessTime = ntime + netUpdateGridRate;

        if (Mathf.Abs(objTransform.position.x - lastTransformPos.x) < NetMapManager.inst.minDisGrid) return;
        if (Mathf.Abs(objTransform.position.z - lastTransformPos.z) < NetMapManager.inst.minDisGrid) return;
    }

    //当离开同步空间,例如道具被玩家拾取
    public void LeaveSpace()
    { 
    
    }

    //进入同步空间
    public void JoinSpace()
    { 
    
    }
}
