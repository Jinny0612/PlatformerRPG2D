using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "PlayAudioEventSO", menuName = "ScriptableObjects/Event/PlayAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    /// <summary>
    /// ��Ҫ���ŵ���Ƶ
    /// </summary>
    public UnityAction<AudioClip> OnAudioPlay;
    /// <summary>
    /// ������Ƶ
    /// </summary>
    /// <param name="audioClip"></param>
    public void CallAudioPlay(AudioClip audioClip)
    {
        OnAudioPlay?.Invoke(audioClip);
    }

}
