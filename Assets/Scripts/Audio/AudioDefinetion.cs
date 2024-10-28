using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ƶ����
/// </summary>
public class AudioDefinetion : MonoBehaviour
{
    /// <summary>
    /// ��Ƶ�����¼�
    /// </summary>
    public PlayAudioEventSO playAudioEvent;
    /// <summary>
    /// ��ƵƬ��
    /// </summary>
    public AudioClip clip;
    /// <summary>
    /// �Ƿ񲥷���Ƶ
    /// </summary>
    public bool playOnEnable;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            PlayAudoClip();
        }
    }

    /// <summary>
    /// ������ԴƬ��
    /// </summary>
    public void PlayAudoClip()
    {
        playAudioEvent.OnAudioPlay(clip);
    }
}
