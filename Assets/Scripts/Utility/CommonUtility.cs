using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通用工具类
/// </summary>
public static class CommonUtility
{
    /// <summary>
    /// 启用游戏时玩家输入
    /// </summary>
    public static void EnableGamePlayerContorl()
    {
        PlayerController.Instance.EnablePlayerControl();
    }
    /// <summary>
    /// 禁用游戏时玩家输入
    /// </summary>
    public static void DisableGamePlayerContorl()
    {
        PlayerController.Instance.DisablePlayerControl();
    }
}
