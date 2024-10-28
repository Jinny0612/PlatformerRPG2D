using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// addressable������Ϣ
/// </summary>
[CreateAssetMenu(fileName = "GameSceneSO", menuName = "ScriptableObjects/GameSceneSO")]
public class GameSceneSO : ScriptableObject
{
    /// <summary>
    /// ������Դ����
    /// </summary>
    public AssetReference sceneReference;
    /// <summary>
    /// ��������
    /// </summary>
    public SceneType sceneType;

}

