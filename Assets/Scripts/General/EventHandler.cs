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

    /// <summary>
    /// Ѫ���仯
    /// </summary>
    public static Action<Character> OnHealthChangeEvent;
    /// <summary>
    /// Ѫ���仯
    /// </summary>
    /// <param name="character"></param>
    public static void CallHealthChangeEvent(Character character)
    {
        OnHealthChangeEvent?.Invoke(character);
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

    #region player ui���

    /// <summary>
    /// ����player״̬��
    /// </summary>
    public static Action<GameSceneSO> OnSetPlayerStatusBarEvent;

    public static void CallSetPlayerStatusBarEvent(GameSceneSO gameScene)
    {
        OnSetPlayerStatusBarEvent?.Invoke(gameScene);
    }

    #endregion

    #region Enemy UI���

    public static Action<Character,Enemy,GameObject> OnEnemyHealthUIBarChangeEvent;

    public static void CallEnemyHealthUIBarChangeEvent(Character character,Enemy enemy,GameObject healthBar)
    {
        OnEnemyHealthUIBarChangeEvent?.Invoke(character,enemy,healthBar);
    }

    public static Action<Enemy> OnDestroyHealthUIBarEvent;

    public static void CallDestroyHealthUIBarEvent(Enemy enemy)
    {
        OnDestroyHealthUIBarEvent?.Invoke(enemy);
    }

    #endregion

}
