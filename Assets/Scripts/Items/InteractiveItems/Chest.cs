using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 宝箱控制
/// </summary>
public class Chest : MonoBehaviour,IInteractable
{
    public UnityEvent openChest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 打开操作
    /// </summary>
    public void TriggerAction()
    {
        //todo: 打开箱子、播放声音、打开ui显示物品
        openChest?.Invoke();
    }
}
