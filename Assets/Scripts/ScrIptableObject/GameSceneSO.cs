using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// addressable场景信息
/// </summary>
[CreateAssetMenu(fileName = "GameSceneSO", menuName = "ScriptableObjects/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    /// <summary>
    /// 场景资源引用
    /// </summary>
    public AssetReference sceneReference;
    /// <summary>
    /// 场景类型
    /// </summary>
    public SceneType sceneType;

}

