using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CharacterEventSO",menuName = "ScriptableObjects/Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    /// <summary>
    /// 绑定对应的事件
    /// </summary>
    public UnityAction<Character> OnEventCalled;

    /// <summary>
    /// 调用
    /// </summary>
    /// <param name="character"></param>
    public void CallEvent(Character character)
    {
        OnEventCalled?.Invoke(character);
    }
}
