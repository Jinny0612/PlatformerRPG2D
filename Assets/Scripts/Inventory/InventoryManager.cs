using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 库存管理
/// </summary>
public class InventoryManager : SingletonMonoBehvior<InventoryManager>
{
    /// <summary>
    /// 配置好的物品详情
    /// </summary>
    public ItemDetailSO itemDetailSO;
    /// <summary>
    /// 根据物品详情列表转换为的物品字典
    /// </summary>
    private Dictionary<int,ItemDetails> itemDetailDictionary = new Dictionary<int,ItemDetails>();

    protected override void Awake()
    {
        base.Awake();
        InitItemDetailDictionary();
    }

    /// <summary>
    /// 初始化物品详情字典
    /// </summary>
    private void InitItemDetailDictionary()
    {
        if(itemDetailSO != null)
        {
            foreach(ItemDetails itemDetails in itemDetailSO.Details)
            {
                itemDetailDictionary[itemDetails.itemCode] = itemDetails;
            }
        }
    }

    /// <summary>
    /// 根据物品代码获取物品详情
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public ItemDetails GetItemDetailByCode(int code)
    {
        if(itemDetailDictionary.Count == 0)
        {
            InitItemDetailDictionary();
        }
        return itemDetailDictionary[code];
    }
}
