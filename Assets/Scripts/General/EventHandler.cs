using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件处理
/// </summary>
public static class EventHandler
{
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

    /// <summary>
    /// 定义获取player坐标的委托
    /// </summary>
    /// <returns></returns>
    public delegate Vector3 GetPlayerPosition();
    /// <summary>
    /// 创建获取player坐标的委托对象
    /// </summary>
    public static event GetPlayerPosition OnGetPlayerPosition;

    public static Vector3 CallGetPlayerPosition()
    {
        return (Vector3)(OnGetPlayerPosition?.Invoke());
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

    #endregion

    #region Enemy UI相关

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
