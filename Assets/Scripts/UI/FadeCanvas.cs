using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// 场景切换时淡入淡出控制
/// </summary>
public class FadeCanvas : MonoBehaviour
{
    [Header("事件监听")]
    /// <summary>
    /// 场景淡入淡出事件监听
    /// </summary>
    public FadeEventSO fadeEvent;

    /// <summary>
    /// 淡入淡出的图片
    /// </summary>
    public Image fadeImage;

    private void OnEnable()
    {
        fadeEvent.OnEventCalled += OnFadeEvent;
    }

    private void OnDisable()
    {
        fadeEvent.OnEventCalled -= OnFadeEvent;
    }

    /// <summary>
    /// 淡入淡出事件
    /// </summary>
    /// <param name="target">目标颜色</param>
    /// <param name="duration">淡入淡出的持续时间</param>
    private void OnFadeEvent(Color target, float duration,bool fadeIn)
    {
        // 在持续时间内过渡到指定的颜色
        fadeImage.DOBlendableColor(target, duration);
    }
}
