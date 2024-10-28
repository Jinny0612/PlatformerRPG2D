using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��������
/// </summary>
[CreateAssetMenu(fileName = "SceneLoadEventSO", menuName = "ScriptableObjects/Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector3, bool> LoadRequestEvent;

    /// <summary>
    /// ���ó�����������
    /// </summary>
    /// <param name="locationToLoad">Ҫ���صĳ���</param>
    /// <param name="positionToGo">�³���player��ʼ������</param>
    /// <param name="fadeScreen">�Ƿ�������뽥��Ч��</param>
    public void CallLoadRequestEvent(GameSceneSO locationToLoad, Vector3 positionToGo, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad,positionToGo,fadeScreen);
    }
}
