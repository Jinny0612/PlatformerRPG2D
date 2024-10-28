using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �������κβ������¼�
/// </summary>
[CreateAssetMenu(fileName = "VoidEventSO",menuName = "ScriptableObjects/Event/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEventCalled;

    public void CallEvent()
    {
        OnEventCalled?.Invoke();
    }
}
