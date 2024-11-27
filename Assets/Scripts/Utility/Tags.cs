using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 标签管理
/// </summary>
public static class Tags 
{
    /// <summary>
    /// 敌人
    /// </summary>
    public static string ENEMY = "Enemy";
    /// <summary>
    /// player 攻击范围
    /// </summary>
    public static string ATTACKAREA = "AttackArea";
    /// <summary>
    /// 玩家角色
    /// </summary>
    public static string PLAYER = "Player";
    /// <summary>
    /// 摄像机边界  每个场景都会设置
    /// </summary>
    public static string CONFINERBOUNDS = "ConfinerBounds";
    /// <summary>
    /// 水面
    /// </summary>
    public static string WATER = "Water";
    /// <summary>
    /// 尖刺 或触碰了会受伤的场景物体
    /// </summary>
    public static string SPIKE = "Spike";
    /// <summary>
    /// 可互动的物体，比如门，宝箱等
    /// </summary>
    public static string INTERACTIVE = "Interactive";
    /// <summary>
    /// enemy血量条
    /// </summary>
    public static string ENEMYHEALTHBAR = "EnemyHealthBar";
    /// <summary>
    /// 可拾取物品
    /// </summary>
    public static string PICKABLEITEM = "PickableItem";
    /// <summary>
    /// 击杀敌人掉落物品的父物体
    /// </summary>
    public static string DROPITEMPARENT = "DropItemParent";
}
