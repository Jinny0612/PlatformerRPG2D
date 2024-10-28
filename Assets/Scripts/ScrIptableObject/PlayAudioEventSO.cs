using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "PlayAudioEventSO", menuName = "ScriptableObjects/Event/PlayAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    /// <summary>
    /// 绑定要播放的音频
    /// </summary>
    public UnityAction<AudioClip> OnAudioPlay;
    /// <summary>
    /// 播放音频
    /// </summary>
    /// <param name="audioClip"></param>
    public void CallAudioPlay(AudioClip audioClip)
    {
        OnAudioPlay?.Invoke(audioClip);
    }

}
