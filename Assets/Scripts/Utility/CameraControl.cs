using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    [Header("事件监听")]
    public VoidEventSO afterSceneLoadedEvent;

    [Header("基础参数")]
    /// <summary>
    /// 摄像机边界
    /// </summary>
    private CinemachineConfiner2D confiner2D;
    /// <summary>
    /// 摄像机振动源
    /// </summary>
    public CinemachineImpulseSource impulseSource;
    /// <summary>
    /// 摄像机抖动事件
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
    /// 场景加载完成后触发的事件
    /// </summary>
    private void OnAfterSceneLoadedEvent()
    {
        GetNewCameraBounds();
      
    }


    /// <summary>
    /// 获取新的摄像机边界
    /// </summary>
    private void GetNewCameraBounds()
    {
        // 获取当前场景摄像机边界对象
        GameObject obj = GameObject.FindGameObjectWithTag(Tags.CONFINERBOUNDS);
        if (obj != null)
        {
            // 获取碰撞体并设置给摄像机
            confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
            // 切换摄像机边界时必须清理边界图形缓存
            confiner2D.InvalidateCache();
        }
    }

    /// <summary>
    /// 摄像机抖动事件
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnCameraShakeEvent()
    {
        // 产生震动
        impulseSource.GenerateImpulse();
    }
}
