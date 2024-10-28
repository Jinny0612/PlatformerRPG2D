using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "CharacterEventSO",menuName = "ScriptableObjects/Event/CharacterEventSO")]
public class CharacterEventSO : ScriptableObject
{
    /// <summary>
    /// �󶨶�Ӧ���¼�
    /// </summary>
    public UnityAction<Character> OnEventCalled;

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="character"></param>
    public void CallEvent(Character character)
    {
        OnEventCalled?.Invoke(character);
    }
}
