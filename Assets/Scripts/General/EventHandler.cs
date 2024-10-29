using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �¼�����
/// </summary>
public static class EventHandler
{
    #region player���������¼�
    /// <summary>
    /// player�����¼�
    /// </summary>
    public static Action OnPlayerAttack;
    public static void CallPlayerAttack()
    {
        OnPlayerAttack?.Invoke();
    }

    /// <summary>
    /// player�����¼�
    /// </summary>
    public static Action OnPlayerDash;
    public static void CallPlayerDash()
    {
        OnPlayerDash?.Invoke();
    }
    #endregion

    /// <summary>
    /// �����ȡplayer�����ί��
    /// </summary>
    /// <returns></returns>
    public delegate Vector3 GetPlayerPosition();
    /// <summary>
    /// ������ȡplayer�����ί�ж���
    /// </summary>
    public static event GetPlayerPosition OnGetPlayerPosition;

    public static Vector3 CallGetPlayerPosition()
    {
        return (Vector3)(OnGetPlayerPosition?.Invoke());
    }


    #region �����л����

    /// <summary>
    /// ��ʼ����Ϸ�¼�ί��
    /// </summary>
    public static Action OnNewGameEvent;
    /// <summary>
    /// ִ�п�ʼ��Ϸί��
    /// </summary>
    public static void CallNewGameEvent()
    {
        OnNewGameEvent?.Invoke();
    }

    /// <summary>
    /// ��������ί��
    /// </summary>
    public static Action<GameSceneSO, Vector3, bool> OnSceneLoadEvent;
    /// <summary>
    /// ���ó�������ί��
    /// </summary>
    /// <param name="gameScene"></param>
    /// <param name="playerPosition"></param>
    /// <param name="isFaded"></param>
    public static void CallSceneLoadEvent(GameSceneSO gameScene, Vector3 playerPosition, bool isFaded)
    {
        OnSceneLoadEvent?.Invoke(gameScene, playerPosition, isFaded);
    }

    /// <summary>
    /// ����ж��ί��
    /// </summary>
    public static Action<GameSceneSO, Vector3, bool> OnSceneUnloadEvent;
    /// <summary>
    /// ���ó���ж��ί��
    /// </summary>
    /// <param name="gameScene"></param>
    /// <param name="playerPosition"></param>
    /// <param name="isFaded"></param>
    public static void CallSceneUnloadEvent(GameSceneSO gameScene, Vector3 playerPosition, bool isFaded)
    {
        OnSceneLoadEvent?.Invoke(gameScene, playerPosition, isFaded);
    }

    /// <summary>
    /// �������سɹ�����¼�
    /// </summary>
    public static Action OnAfterSceneLoadEvent;
    /// <summary>
    /// ���ó������سɹ����ί��
    /// </summary>
    public static void CallAfterSceneLoadEvent()
    {
        OnAfterSceneLoadEvent?.Invoke();
    }

    #endregion

    #region Character���

    /// <summary>
    /// �л���������character������Ϣ
    /// </summary>
    public static Action OnResetCharacterBasicInfoWhenLoadSceneEvent;
    /// <summary>
    /// �л���������character������Ϣ
    /// </summary>
    public static void CallResetCharacterBasicInfoWhenLoadSceneEvent()
    {
        OnResetCharacterBasicInfoWhenLoadSceneEvent?.Invoke();
    }

    #endregion

}
