using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景切换，场景传送点
/// </summary>
public class Teleport : MonoBehaviour,IInteractable
{
    /// <summary>
    /// 新场景的角色坐标
    /// </summary>
    public Vector3 positionToGo;
    /// <summary>
    /// 新场景
    /// </summary>
    public GameSceneSO sceneToGo;
    /// <summary>
    /// 场景加载事件
    /// </summary>
    public SceneLoadEventSO loadEventSO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 触发场景切换行为
    /// </summary>
    public void TriggerAction()
    {
        //Debug.Log("try to teleport");
        //Debug.Log(loadSceneEvent);
        // 调用场景加载事件
        //loadEventSO.CallLoadRequestEvent(sceneToGo, positionToGo, true);

        EventHandler.CallSceneLoadEvent(sceneToGo,positionToGo, true);
    }
}
