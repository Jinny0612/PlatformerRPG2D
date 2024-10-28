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


}
