using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件处理
/// </summary>
public static class EventHandler
{

    #region Player

    #region player动画触发事件
    /// <summary>
    /// player攻击事件
    /// </summary>
    public static Action OnPlayerAttack;
    public static void CallPlayerAttack()
    {
        OnPlayerAttack?.Invoke();
    }

    /// <summary>
    /// player闪现事件
    /// </summary>
    public static Action OnPlayerDash;
    public static void CallPlayerDash()
    {
        OnPlayerDash?.Invoke();
    }
    #endregion

    #region player ui相关

    /// <summary>
    /// 设置player状态栏
    /// </summary>
    public static Action<GameSceneSO> OnSetPlayerStatusBarEvent;

    public static void CallSetPlayerStatusBarEvent(GameSceneSO gameScene)
    {
        OnSetPlayerStatusBarEvent?.Invoke(gameScene);
    }

    /// <summary>
    /// 玩家等级变化
    /// </summary>
    public static Action<int> OnPlayerLevelChangeEvent;

    public static void CallPlayerLevelChangeEvent(int level)
    {
        OnPlayerLevelChangeEvent?.Invoke(level);
    }

    /// <summary>
    /// 玩家金币数量变化
    /// </summary>
    public static Action<int> OnPlayerGoldCoinChangeEvent;

    public static void CallPlayerGoldCoinChangeEvent(int coinQuantity)
    {
        OnPlayerGoldCoinChangeEvent?.Invoke(coinQuantity);
    }

    /// <summary>
    /// 创建提醒通知委托
    /// </summary>
    public static Action<NotificationType, int, int> OnCreateNotificationEvent;
    public static void CallCreateNotificationEvent(NotificationType type,int quantity,int itemCode)
    {
        OnCreateNotificationEvent?.Invoke(type, quantity, itemCode);
    }

    /// <summary>
    /// 通知显示倒计时结束时删除通知
    /// </summary>
    public static Action<GameObject> OnDeleteNotificationAfterTimeCount;
    public static void CallDeleteNotificationAfterTimeCount(GameObject notification)
    {
        OnDeleteNotificationAfterTimeCount?.Invoke(notification);
    }

    #endregion

    /// <summary>
    /// 定义获取player坐标的委托
    /// </summary>
    /// <returns></returns>
    public delegate Vector3 GetPlayerPosition();
    /// <summary>
    /// 创建获取player坐标的委托对象
    /// </summary>
    public static event GetPlayerPosition OnGetPlayerPosition;
    /// <summary>
    /// 调用获取player坐标委托
    /// </summary>
    /// <returns></returns>
    public static Vector3 CallGetPlayerPosition()
    {
        return (Vector3)(OnGetPlayerPosition?.Invoke());
    }

    /// <summary>
    /// player经验变化
    /// </summary>
    public static Action<int> OnSetPlayerExpEvent;

    public static void CallSetPlayerExpEvent(int exp)
    {
        OnSetPlayerExpEvent?.Invoke(exp);
    }

    /// <summary>
    /// player金币变化
    /// </summary>
    public static Action<int> OnSetPlayerCoinEvent;

    public static void CallSetPlayerCoinEvent(int coin)
    {
        OnSetPlayerCoinEvent?.Invoke(coin);
    }

    #endregion


    #region Character相关

    /// <summary>
    /// 切换场景重置character基本信息
    /// </summary>
    public static Action OnResetCharacterBasicInfoWhenLoadSceneEvent;
    /// <summary>
    /// 切换场景重置character基本信息
    /// </summary>
    public static void CallResetCharacterBasicInfoWhenLoadSceneEvent()
    {
        OnResetCharacterBasicInfoWhenLoadSceneEvent?.Invoke();
    }

    /// <summary>
    /// 血量变化
    /// </summary>
    public static Action<Character> OnHealthChangeEvent;
    /// <summary>
    /// 血量变化
    /// </summary>
    /// <param name="character"></param>
    public static void CallHealthChangeEvent(Character character)
    {
        OnHealthChangeEvent?.Invoke(character);
    }
    #endregion



    #region Enemy相关

    #region Enemy基础相关

    /// <summary>
    /// enemy受伤委托
    /// </summary>
    public static Action<Transform> OnEnemyGetHurtEvent;

    /// <summary>
    /// 调用enemy受伤委托
    /// </summary>
    /// <param name="transform"></param>
    public static void CallEnemyGetHurtEvent(Transform transform)
    {
        OnEnemyGetHurtEvent?.Invoke(transform);
    }

    /// <summary>
    /// enemy死亡委托
    /// </summary>
    public static Action OnEnemyDeadEvent;
    /// <summary>
    /// 调用Enemy死亡委托
    /// </summary>
    public static void CallEnemyDeadEvent()
    {
        OnEnemyDeadEvent?.Invoke();
    }

    #endregion

    #region Enemy UI相关

    /// <summary>
    /// enemy血条更新
    /// </summary>
    public static Action<Character, Enemy,GameObject> OnEnemyHealthUIBarChangeEvent;

    /// <summary>
    /// enemy血条更新
    /// </summary>
    /// <param name="character"></param>
    /// <param name="enemy"></param>
    /// <param name="healthBar"></param>
    public static void CallEnemyHealthUIBarChangeEvent(Character character,Enemy enemy,GameObject healthBar)
    {
        OnEnemyHealthUIBarChangeEvent?.Invoke(character,enemy,healthBar);
    }

    #region 暂时不用 同EnemyCharacter脚本一样
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

    #region 场景切换相关

    /// <summary>
    /// 开始新游戏事件委托
    /// </summary>
    public static Action OnNewGameEvent;
    /// <summary>
    /// 执行开始游戏委托
    /// </summary>
    public static void CallNewGameEvent()
    {
        OnNewGameEvent?.Invoke();
    }

    /// <summary>
    /// 场景加载委托
    /// </summary>
    public static Action<GameSceneSO, Vector3, bool> OnSceneLoadEvent;
    /// <summary>
    /// 调用场景加载委托
    /// </summary>
    /// <param name="gameScene"></param>
    /// <param name="playerPosition"></param>
    /// <param name="isFaded"></param>
    public static void CallSceneLoadEvent(GameSceneSO gameScene, Vector3 playerPosition, bool isFaded)
    {
        OnSceneLoadEvent?.Invoke(gameScene, playerPosition, isFaded);
    }

    /// <summary>
    /// 场景卸载委托
    /// </summary>
    public static Action<GameSceneSO, Vector3, bool> OnSceneUnloadEvent;
    /// <summary>
    /// 调用场景卸载委托
    /// </summary>
    /// <param name="gameScene"></param>
    /// <param name="playerPosition"></param>
    /// <param name="isFaded"></param>
    public static void CallSceneUnloadEvent(GameSceneSO gameScene, Vector3 playerPosition, bool isFaded)
    {
        OnSceneLoadEvent?.Invoke(gameScene, playerPosition, isFaded);
    }

    /// <summary>
    /// 场景加载成功后的事件
    /// </summary>
    public static Action OnAfterSceneLoadEvent;
    /// <summary>
    /// 调用场景加载成功后的委托
    /// </summary>
    public static void CallAfterSceneLoadEvent()
    {
        OnAfterSceneLoadEvent?.Invoke();
    }

    #endregion

    #region 库存相关事件

    /*/// <summary>
    /// 库存更新事件
    /// </summary>
    public static event Action<InventoryLocation, List<InventoryItem>> OnInventoryUpdateEvent;

    /// <summary>
    /// 调用库存更新事件
    /// </summary>
    /// <param name="location"></param>
    /// <param name="items"></param>
    public static void CallInventoryUpdateEvent(InventoryLocation location, List<InventoryItem> items)
    {
        OnInventoryUpdateEvent?.Invoke(location, items);
    }*/

    /// <summary>
    /// 显示库存UI内容事件
    /// </summary>
    public static event Action<InventoryLocation, List<InventoryItem>> OnInventoryUpdateEvent;
    /// <summary>
    /// 调用显示库存UI内容事件
    /// </summary>
    /// <param name="location"></param>
    /// <param name="items"></param>
    public static void CallInventoryUpdateEvent(InventoryLocation location,List<InventoryItem> items)
    {
        OnInventoryUpdateEvent?.Invoke(location, items);
    }

    #endregion
}
