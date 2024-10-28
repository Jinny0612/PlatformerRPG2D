using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FadeEventSO", menuName ="ScriptableObjects/Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color,float,bool> OnEventCalled;

    /// <summary>
    /// 调用事件
    /// </summary>
    /// <param name="target">目标颜色</param>
    /// <param name="duration">持续时间</param>
    /// <param name="fadeIn">是否淡入</param>
    public void CallEvent(Color target, float duration, bool fadeIn)
    {
        OnEventCalled?.Invoke(target, duration, fadeIn);
    }

    /// <summary>
    /// 淡入  逐渐变黑
    /// </summary>
    /// <param name="duration"></param>
    public void FadeIn(float duration)
    {
        CallEvent(Color.black, duration, true);
    }

    /// <summary>
    /// 淡出  逐渐变透明
    /// </summary>
    /// <param name="duration"></param>
    /// <returns></returns>
    public void FadeOut(float duration)
    {
        CallEvent(Color.clear, duration, false);
    }
}
