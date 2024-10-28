using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FadeEventSO", menuName ="ScriptableObjects/Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color,float,bool> OnEventCalled;

    /// <summary>
    /// �����¼�
    /// </summary>
    /// <param name="target">Ŀ����ɫ</param>
    /// <param name="duration">����ʱ��</param>
    /// <param name="fadeIn">�Ƿ���</param>
    public void CallEvent(Color target, float duration, bool fadeIn)
    {
        OnEventCalled?.Invoke(target, duration, fadeIn);
    }

    /// <summary>
    /// ����  �𽥱��
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        CallEvent(Color.black, duration, true);
    }

    /// <summary>
    /// ����  �𽥱�͸��
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public void FadeOut(float duration)
    {
        CallEvent(Color.clear, duration, false);
    }
}
