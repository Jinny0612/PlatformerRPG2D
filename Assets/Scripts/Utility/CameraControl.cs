using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    [Header("�¼�����")]
    public VoidEventSO afterSceneLoadedEvent;

    [Header("��������")]
    /// <summary>
    /// ������߽�
    /// </summary>
    private CinemachineConfiner2D confiner2D;
    /// <summary>
    /// �������Դ
    /// </summary>
    public CinemachineImpulseSource impulseSource;
    /// <summary>
    /// ����������¼�
    /// </summary>
    public VoidEventSO cameraShakeEvent;

    private void Awake()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        cameraShakeEvent.OnEventCalled += OnCameraShakeEvent;
        //afterSceneLoadedEvent.OnEventCalled += OnAfterSceneLoadedEvent;

        EventHandler.OnAfterSceneLoadEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventCalled -= OnCameraShakeEvent;
        //afterSceneLoadedEvent.OnEventCalled -= OnAfterSceneLoadedEvent;

        EventHandler.OnAfterSceneLoadEvent -= OnAfterSceneLoadedEvent;
    }

    /// <summary>
    /// ����������ɺ󴥷����¼�
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        GetNewCameraBounds();
      
    }


    /// <summary>
    /// ��ȡ�µ�������߽�
    /// </summary>
    private void GetNewCameraBounds()
    {
        // ��ȡ��ǰ����������߽����
        GameObject obj = GameObject.FindGameObjectWithTag(Tags.CONFINERBOUNDS);
        if (obj != null)
        {
            // ��ȡ��ײ�岢���ø������
            confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
            // �л�������߽�ʱ��������߽�ͼ�λ���
            confiner2D.InvalidateCache();
        }
    }

    /// <summary>
    /// ����������¼�
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnCameraShakeEvent()
    {
        // ������
        impulseSource.GenerateImpulse();
    }
}
