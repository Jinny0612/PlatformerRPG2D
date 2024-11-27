using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 库存管理
/// </summary>
public class InventoryManager : SingletonMonoBehvior<InventoryManager>
{
    /// <summary>
    /// 库存列表 对应不同的库存
    /// 下标为InventoryLocation
    /// </summary>
    public List<InventoryItem>[] inventoryLists;

    /// <summary>
    /// 库存对应的容量大小  下标为InventoryLocation
    /// </summary>
    public int[] inventoryListCapacityIntArray;

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
        // 初始化物品详情
        InitItemDetailDictionary();

        // 创建玩家库存清单 
        CreateInventoryLists();
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
    /// 根据库存类型获取库存物品清单
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public List<InventoryItem> GetInventoryListByInventoryType(InventoryLocation location)
    {
        return inventoryLists[(int)location];
    }

    /// <summary>
    /// 根据库存类型获取库存容量大小
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public int GetInventoryCapacityByInventoryType(InventoryLocation location)
    {
        return inventoryListCapacityIntArray[(int)location];
    }

    /// <summary>
    /// 设置库存容量大小
    /// </summary>
    /// <param name="location"></param>
    /// <param name="capacity"></param>
    public void SetInventoryCapation(InventoryLocation location, int capacity)
    {
        inventoryListCapacityIntArray[(int)location] = capacity;
    }

    /// <summary>
    /// 根据物品代码获取物品详情
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public ItemDetails GetItemDetailByCode(int code)
    {
        ItemDetails itemDetails = null;
        if(itemDetailDictionary.TryGetValue(code,out itemDetails))
        {
            return itemDetails;
        }
        return null;
    }

    /// <summary>
    /// 创建玩家库存清单
    /// </summary>
    private void CreateInventoryLists()
    {
        // 数组大小根据库存位置中的count值来创建，count为枚举值中最后一个元素，因此count的数值就是库存类型的数量
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];

        for(int i = 0; i < (int)InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }

        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];

        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    /// <summary>
    /// 添加物品到库存中，并且删除这个游戏物体
    /// </summary>
    /// <param name="location"></param>
    /// <param name="item"></param>
    /// <param name="gameObjectToDelete"></param>
    public void AddItem(InventoryLocation location, Item item, GameObject gameObjectToDelete)
    {
        AddItem(location, item);
        Destroy(gameObjectToDelete);
    }

    /// <summary>
    /// 添加物品到库存中
    /// </summary>
    /// <param name="location"></param>
    /// <param name="item"></param>
    public void AddItem(InventoryLocation location,Item item)
    {
        int itemCode = item.ItemCode;
        // 根据库存位置获取对应的物品列表
        List<InventoryItem> inventories = inventoryLists[(int)location];
        // 根据物品代码从清单中得到物品在库存中的位置
        int itemPosition = FindItemInInventory(location, itemCode);

        if (itemPosition != -1)
        {
            // 库存中已经有这个物品，只需要更新数量
            AddItemAtPosition(inventories, itemCode, itemPosition);
        }
        else
        {
            // 库存中没有这个物品，将物品添加到库存中
            AddItemAtPosition(inventories, itemCode);
        }

        // 发送库存更新消息
        EventHandler.CallInventoryUpdateEvent(location, inventoryLists[(int)location]);
    }

    /// <summary>
    /// 根据物品代码从指定库存位置的物品清单中获取物品所在的位置
    /// </summary>
    /// <param name="location"></param>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    private int FindItemInInventory(InventoryLocation location, int itemCode)
    {
        List<InventoryItem> inventories = inventoryLists[(int)location];
        for(int i= 0; i < inventories.Count; i++)
        {
            if (inventories[i].itemCode == itemCode)
            {
                return i;
            }
        }
        return -1;

    }

    /// <summary>
    /// 向指定库存物品清单中添加新的物品
    /// </summary>
    /// <param name="inventories"></param>
    /// <param name="itemCode"></param>
    private void AddItemAtPosition(List<InventoryItem> inventories, int itemCode)
    {
        InventoryItem item = new InventoryItem();

        item.itemCode = itemCode;
        item.itemQuantity = 1;
        
        //todo: 后面再看怎么实现放入第一个空位中
        inventories.Add(item);


        DebugShowInventoryList(inventories);
    }

    /// <summary>
    /// 在指定库存物品清单中更新物品的数量
    /// </summary>
    /// <param name="inventories"></param>
    /// <param name="itemCode"></param>
    /// <param name="itemPosition"></param>
    private void AddItemAtPosition(List<InventoryItem> inventories, int itemCode, int itemPosition)
    {
        InventoryItem item = inventories[itemPosition];
        item.itemQuantity += 1;
        inventories[itemPosition] = item;

        DebugShowInventoryList(inventories);
    }

    /// <summary>
    /// 删除物品
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="itemCode"></param>
    public void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        // 获取对应库存物品
        List<InventoryItem> inventoryItems = inventoryLists[(int)inventoryLocation];
        // 获取物品所在的位置
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);
        if(itemPosition != -1)
        {
            RemoveItemAtPosition(inventoryItems,itemCode,itemPosition);
        }
        // 更新UI显示
        EventHandler.CallShowInventoryEvent(inventoryLocation, inventoryItems);

    }

    /// <summary>
    /// 删除指定位置物体
    /// </summary>
    /// <param name="inventoryItems"></param>
    /// <param name="itemCode"></param>
    /// <param name="itemPosition"></param>
    private void RemoveItemAtPosition(List<InventoryItem> inventoryItems, int itemCode, int itemPosition)
    {
        InventoryItem item = new InventoryItem();

        int quantity = inventoryItems[itemPosition].itemQuantity -1;

        if(quantity > 0)
        {
            // 更新物体数量
            item.itemQuantity = quantity;
            item.itemCode = itemCode;
            inventoryItems[itemPosition] = item;
        }
        else
        {
            // 数量降低为0，删除库存中这个物体
            inventoryItems.Remove(item);
            
        }
    }

    private void DebugShowInventoryList(List<InventoryItem> inventories)
    {
        foreach(InventoryItem item in inventories)
        {
            Debug.Log("Item Description: " + InventoryManager.Instance.GetItemDetailByCode(item.itemCode).itemDescription 
                + "     item quantity = " + item.itemQuantity);
        }
        Debug.Log("------------------ 背包信息打印结束 ------------------");
    }

}
