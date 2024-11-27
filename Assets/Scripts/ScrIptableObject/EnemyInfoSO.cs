using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfoSO",menuName = "ScriptableObjects/ObjectsInfo/EnemyInfoSO")]
public class EnemyInfoSO : ScriptableObject
{
    public List<EnemyInfo> Enemies;
}

[System.Serializable]
public class EnemyInfo
{
    /// <summary>
    /// 敌人编号，不同类型的敌人编号不同
    /// </summary>
    public int enemyCode;
    /// <summary>
    /// 敌人名称
    /// </summary>
    public string enemyName;
    /// <summary>
    /// 击杀经验
    /// </summary>
    public int killExp;
    /// <summary>
    /// 掉落最小金币数
    /// </summary>
    public int minCoin;
    /// <summary>
    /// 掉落最大金币数
    /// </summary>
    public int maxCoin;
    /// <summary>
    /// 敌人掉落的物品列表
    /// </summary>
    public List<EnemyDropItem> enemyDropItemList;
    
}

/// <summary>
/// 敌人死亡掉落的物品s
/// </summary>
[System.Serializable]
public class EnemyDropItem
{
    /// <summary>
    /// 物品代码
    /// </summary>
    [ItemCodeDescription]
    public int itemCode;
    /// <summary>
    /// 物品数量 非随机数量时使用
    /// </summary>
    public int itemQuantity;
    /// <summary>
    /// 物品数量是否是随机数
    /// </summary>
    public bool isRandom;
    /// <summary>
    /// 最小数量
    /// </summary>
    public int minItemQuantity;
    /// <summary>
    /// 最大数量
    /// </summary>
    public int maxItemQuantity;
    /// <summary>
    /// 掉落概率 0-1范围
    /// </summary>
    public float dropChance;
}
