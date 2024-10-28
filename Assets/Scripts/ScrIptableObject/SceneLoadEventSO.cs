using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 场景加载
/// </summary>
[CreateAssetMenu(fileName = "SceneLoadEventSO", menuName = "ScriptableObjects/Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;

    /// <summary>
    /// 调用场景加载请求
    /// </summary>
    /// <param name="locationToLoad">要加载的场景</param>
    /// <param name="positionToGo">新场景player初始的坐标</param>
    /// <param name="fadeScreen">是否产生渐入渐出效果</param>
    public void CallLoadRequestEvent(GameSceneSO locationToLoad, Vector3 positionToGo, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad,positionToGo,fadeScreen);
    }
}
