using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 物品详情
/// 主要便于在ScriptableObjects中配置
/// </summary>
[System.Serializable]
public class ItemDetails
{
    /// <summary>
    /// 物品代码  唯一
    /// </summary>
    public int itemCode;
    /// <summary>
    /// 物品类型
    /// </summary>
    public ItemType itemType;
    /// <summary>
    /// 物品简介
    /// </summary>
    public string itemDescription;
    /// <summary>
    /// 物品图片
    /// </summary>
    public Sprite itemSprite;
    /// <summary>
    /// 预制体
    /// </summary>
    public GameObject prefab;
    /// <summary>
    /// 物品详细介绍
    /// </summary>
    public string itemLongDescription;
    /// <summary>
    /// 是否可拾取
    /// </summary>
    public bool isPickable;
    /// <summary>
    /// 是否可丢弃
    /// </summary>
    public bool canBeDropped;
    /// <summary>
    /// 物品数值配置列表
    /// </summary>
    public List<ItemValues> itemValuesList;
}

/// <summary>
/// 物品数值类型
/// </summary>
[System.Serializable]
public class ItemValues
{
    /// <summary>
    /// 数值类型
    /// </summary>
    public ItemValuesType valueType;
    /// <summary>
    /// 数值
    /// </summary>
    public float value;
    /// <summary>
    /// 是否百分比，是则value范围0-1
    /// </summary>
    public bool isPercentage;
}
