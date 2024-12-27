using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �¼�����
/// </summary>
public static class EventHandler
{

    #region Player

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

    #region player ui���

    /// <summary>
    /// ����player״̬��
    /// </summary>
    public static Action<GameSceneSO> OnSetPlayerStatusBarEvent;

    public static void CallSetPlayerStatusBarEvent(GameSceneSO gameScene)
    {
        OnSetPlayerStatusBarEvent?.Invoke(gameScene);
    }

    /// <summary>
    /// ��ҵȼ��仯
    /// </summary>
    public static Action<int> OnPlayerLevelChangeEvent;

    public static void CallPlayerLevelChangeEvent(int level)
    {
        OnPlayerLevelChangeEvent?.Invoke(level);
    }

    /// <summary>
    /// ��ҽ�������仯
    /// </summary>
    public static Action<int> OnPlayerGoldCoinChangeEvent;

    public static void CallPlayerGoldCoinChangeEvent(int coinQuantity)
    {
        OnPlayerGoldCoinChangeEvent?.Invoke(coinQuantity);
    }

    /// <summary>
    /// ��������֪ͨί��
    /// </summary>
    public static Action<NotificationType, int, int> OnCreateNotificationEvent;
    public static void CallCreateNotificationEvent(NotificationType type,int quantity,int itemCode)
    {
        OnCreateNotificationEvent?.Invoke(type, quantity, itemCode);
    }

    /// <summary>
    /// ֪ͨ��ʾ����ʱ����ʱɾ��֪ͨ
    /// </summary>
    public static Action<GameObject> OnDeleteNotificationAfterTimeCount;
    public static void CallDeleteNotificationAfterTimeCount(GameObject notification)
    {
        OnDeleteNotificationAfterTimeCount?.Invoke(notification);
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
    /// <summary>
    /// ���û�ȡplayer����ί��
    /// </summary>
    /// <returns></returns>
    public static Vector3 CallGetPlayerPosition()
    {
        return (Vector3)(OnGetPlayerPosition?.Invoke());
    }

    /// <summary>
    /// player����仯
    /// </summary>
    public static Action<int> OnSetPlayerExpEvent;

    public static void CallSetPlayerExpEvent(int exp)
    {
        OnSetPlayerExpEvent?.Invoke(exp);
    }

    /// <summary>
    /// player��ұ仯
    /// </summary>
    public static Action<int> OnSetPlayerCoinEvent;

    public static void CallSetPlayerCoinEvent(int coin)
    {
        OnSetPlayerCoinEvent?.Invoke(coin);
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
    #endregion



    #region Enemy���

    #region Enemy�������

    /// <summary>
    /// enemy����ί��
    /// </summary>
    public static Action<Transform> OnEnemyGetHurtEvent;

    /// <summary>
    /// ����enemy����ί��
    /// </summary>
    /// <param name="transform"></param>
    public static void CallEnemyGetHurtEvent(Transform transform)
    {
        OnEnemyGetHurtEvent?.Invoke(transform);
    }

    /// <summary>
    /// enemy����ί��
    /// </summary>
    public static Action OnEnemyDeadEvent;
    /// <summary>
    /// ����Enemy����ί��
    /// </summary>
    public static void CallEnemyDeadEvent()
    {
        OnEnemyDeadEvent?.Invoke();
    }

    #endregion

    #region Enemy UI���

    /// <summary>
    /// enemyѪ������
    /// </summary>
    public static Action<Character, Enemy,GameObject> OnEnemyHealthUIBarChangeEvent;

    /// <summary>
    /// enemyѪ������
    /// </summary>
    /// <param name="character"></param>
    /// <param name="enemy"></param>
    /// <param name="healthBar"></param>
    public static void CallEnemyHealthUIBarChangeEvent(Character character,Enemy enemy,GameObject healthBar)
    {
        OnEnemyHealthUIBarChangeEvent?.Invoke(character,enemy,healthBar);
    }

    #region ��ʱ���� ͬEnemyCharacter�ű�һ��
    public static Action<EnemyCharacter, Enemy, GameObject> OnEnemyHealthUIBarChangeEventNew;

    public static void CallEnemyHealthUIBarChangeEventNew(EnemyCharacter character, Enemy enemy, GameObject healthBar)
    {
        OnEnemyHealthUIBarChangeEventNew?.Invoke(character, enemy, healthBar);
    }
    #endregion

    public static Action<Enemy> OnDestroyHealthUIBarEvent;

    public static void CallDestroyHealthUIBarEvent(Enemy enemy)
    {
        OnDestroyHealthUIBarEvent?.Invoke(enemy);
    }

    #endregion

    #endregion

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

    #region �������¼�

    /*/// <summary>
    /// �������¼�
    /// </summary>
    public static event Action<InventoryLocation, List<InventoryItem>> OnInventoryUpdateEvent;

    /// <summary>
    /// ���ÿ������¼�
    /// </summary>
    /// <param name="location"></param>
    /// <param name="items"></param>
    public static void CallInventoryUpdateEvent(InventoryLocation location, List<InventoryItem> items)
    {
        OnInventoryUpdateEvent?.Invoke(location, items);
    }*/

    /// <summary>
    /// ��ʾ���UI�����¼�
    /// </summary>
    public static event Action<InventoryLocation, List<InventoryItem>> OnInventoryUpdateEvent;
    /// <summary>
    /// ������ʾ���UI�����¼�
    /// </summary>
    /// <param name="location"></param>
    /// <param name="items"></param>
    public static void CallInventoryUpdateEvent(InventoryLocation location,List<InventoryItem> items)
    {
        OnInventoryUpdateEvent?.Invoke(location, items);
    }

    #endregion
}
