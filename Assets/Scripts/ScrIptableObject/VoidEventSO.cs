using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 不传递任何参数的事件
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
