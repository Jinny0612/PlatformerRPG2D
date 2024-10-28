using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// “Ù∆µ∂®“Â
/// </summary>
public class AudioDefinetion : MonoBehaviour
{
    /// <summary>
    /// “Ù∆µ≤•∑≈ ¬º˛
    /// </summary>
    public PlayAudioEventSO playAudioEvent;
    /// <summary>
    /// “Ù∆µ∆¨∂Œ
    /// </summary>
    public AudioClip clip;
    /// <summary>
    ///  «∑Ò≤•∑≈“Ù∆µ
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
    /// ≤•∑≈“Ù‘¥∆¨∂Œ
    /// </summary>
    public void PlayAudoClip()
    {
        playAudioEvent.OnAudioPlay(clip);
    }
}
