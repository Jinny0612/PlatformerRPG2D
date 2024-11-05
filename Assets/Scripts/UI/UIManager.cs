using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ui����
/// </summary>
public class UIManager : SingletonMonoBehvior<UIManager>
{
    public PlayerStatusBar playerStatusBar;

    [Header("�¼�����")]
    /// <summary>
    /// Ѫ���仯�¼�����   Character����OnHealthChange�¼�
    /// </summary>
    public CharacterEventSO healthEvent;
    /// <summary>
    /// ���������¼�����
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
    /// ����player״̬ui�Ƿ���ʾ
    /// </summary>
    private void SetPlayerStatusBar(GameSceneSO sceneToLoad)
    {
        switch (sceneToLoad.sceneType)
        {
            case SceneType.Menu:
                // menu���治��ʾ
                playerStatusBar.gameObject.SetActive(false);
                break;
            case SceneType.Location: 
                playerStatusBar.gameObject.SetActive(true); 
                break;
            default: break;
        }

    }

    /// <summary>
    /// Ѫ���仯�¼�
    /// </summary>
    /// <param name="character"></param>
    private void OnHealthEvent(Character character)
    {
        // ����ٷֱ�
        float persentage = character.currentHealth / character.maxHealth;
        // ���Ͱٷֱ�

        // playerѪ���仯
        playerStatusBar.OnHealthChange(persentage);

        
    }
}
