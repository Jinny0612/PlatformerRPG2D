using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// �����л�ʱ���뵭������
/// </summary>
public class FadeCanvas : MonoBehaviour
{
    [Header("�¼�����")]
    /// <summary>
    /// �������뵭���¼�����
    /// </summary>
    public FadeEventSO fadeEvent;

    /// <summary>
    /// ���뵭����ͼƬ
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
    /// ���뵭���¼�
    /// </summary>
    /// <param name="target">Ŀ����ɫ</param>
    /// <param name="duration">���뵭���ĳ���ʱ��</param>
    private void OnFadeEvent(Color target, float duration,bool fadeIn)
    {
        // �ڳ���ʱ���ڹ��ɵ�ָ������ɫ
        fadeImage.DOBlendableColor(target, duration);
    }
}
