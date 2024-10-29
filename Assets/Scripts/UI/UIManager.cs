using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ui管理
/// </summary>
public class UIManager : MonoBehaviour
{
    public PlayerStatusBar playerStatusBar;

    [Header("事件监听")]
    /// <summary>
    /// 血量变化事件监听
    /// </summary>
    public CharacterEventSO healthEvent;
    /// <summary>
    /// 场景加载事件监听
    /// </summary>
    public SceneLoadEventSO sceneLoadEvent;

    private void OnEnable()
    {
        healthEvent.OnEventCalled += OnHealthEvent;
        //sceneLoadEvent.LoadRequestEvent += OnSceneLoadEvent;

        EventHandler.OnSceneUnloadEvent += OnSceneLoadEvent;
    }

    private void OnDisable()
    {
        healthEvent.OnEventCalled -= OnHealthEvent;
        //sceneLoadEvent.LoadRequestEvent -= OnSceneLoadEvent;

        EventHandler.OnSceneUnloadEvent -= OnSceneLoadEvent;
    }

    /// <summary>
    /// 场景加载事件
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnSceneLoadEvent(GameSceneSO sceneToLoad, Vector3 arg1, bool arg2)
    {
        switch (sceneToLoad.sceneType)
        {
            case SceneType.Menu:
                // menu界面不显示
                playerStatusBar.gameObject.SetActive(false);
                break;
            case SceneType.Location: 
                playerStatusBar.gameObject.SetActive(true); 
                break;
            default: break;
        }

        
        
    }

    /// <summary>
    /// 血量变化事件
    /// </summary>
    /// <param name="character"></param>
    private void OnHealthEvent(Character character)
    {
        // 计算百分比
        float persentage = character.currentHealth / character.maxHealth;
        // 发送百分比
        //todo: 后面再看如何降低耦合
        playerStatusBar.OnHealthChange(persentage);
    }
}
