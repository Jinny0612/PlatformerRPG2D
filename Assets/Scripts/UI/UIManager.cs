using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ui管理
/// </summary>
public class UIManager : SingletonMonoBehvior<UIManager>
{
    public PlayerStatusBar playerStatusBar;

    [Header("事件监听")]
    /// <summary>
    /// 血量变化事件监听   Character绑定了OnHealthChange事件
    /// </summary>
    public CharacterEventSO healthEvent;
    /// <summary>
    /// 场景加载事件监听
    /// </summary>
    public SceneLoadEventSO sceneLoadEvent;

    private void OnEnable()
    {
        //healthEvent.OnEventCalled += OnHealthEvent;
        EventHandler.OnHealthChangeEvent += OnHealthEvent;

        //sceneLoadEvent.LoadRequestEvent += SetPlayerStatusBar;
        EventHandler.OnSetPlayerStatusBarEvent += SetPlayerStatusBar;
    }

    private void OnDisable()
    {
        //healthEvent.OnEventCalled -= OnHealthEvent;
        EventHandler.OnHealthChangeEvent -= OnHealthEvent;


        //sceneLoadEvent.LoadRequestEvent -= SetPlayerStatusBar;
        EventHandler.OnSetPlayerStatusBarEvent -= SetPlayerStatusBar;
    }

    /// <summary>
    /// 设置player状态ui是否显示
    /// </summary>
    private void SetPlayerStatusBar(GameSceneSO sceneToLoad)
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

        // player血量变化
        playerStatusBar.OnHealthChange(persentage);

        
    }
}
