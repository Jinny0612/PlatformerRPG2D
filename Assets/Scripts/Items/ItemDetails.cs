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
    /// 物品详细介绍
    /// </summary>
    public string itemLongDescription;
}
