using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// ��Ƶ����
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("�¼�����")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;

    [Header("��Դ")]
    public AudioSource BGMSource;
    public AudioSource FXSource;

    private void OnEnable()
    {
        FXEvent.OnAudioPlay += OnFXAudioEvent;
        BGMEvent.OnAudioPlay += OnBGMAudioEvent;
    }

    private void OnDisable()
    {
        FXEvent.OnAudioPlay -= OnFXAudioEvent;
        BGMEvent.OnAudioPlay -= OnBGMAudioEvent;
    }

    private void OnBGMAudioEvent(AudioClip clip)
    {
        BGMSource.clip = clip;
        BGMSource.Play();
    }

    private void OnFXAudioEvent(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }
}
