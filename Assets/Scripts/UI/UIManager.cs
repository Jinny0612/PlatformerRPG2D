using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ui����
/// </summary>
public class UIManager : MonoBehaviour
{
    public PlayerStatusBar playerStatusBar;

    [Header("�¼�����")]
    /// <summary>
    /// Ѫ���仯�¼�����
    /// </summary>
    public CharacterEventSO healthEvent;
    /// <summary>
    /// ���������¼�����
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
    /// ���������¼�
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
        //todo: �����ٿ���ν������
        playerStatusBar.OnHealthChange(persentage);
    }
}
