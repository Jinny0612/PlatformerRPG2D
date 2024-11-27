using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// </summary>
public class InventoryManager : SingletonMonoBehvior<InventoryManager>
{
    /// <summary>
    /// ����б� ��Ӧ��ͬ�Ŀ��
    /// �±�ΪInventoryLocation
    /// </summary>
    public List<InventoryItem>[] inventoryLists;

    /// <summary>
    /// ����Ӧ��������С  �±�ΪInventoryLocation
    /// </summary>
    public int[] inventoryListCapacityIntArray;

    /// <summary>
    /// ���úõ���Ʒ����
    /// </summary>
    public ItemDetailSO itemDetailSO;
    /// <summary>
    /// ������Ʒ�����б�ת��Ϊ����Ʒ�ֵ�
    /// </summary>
    private Dictionary<int,ItemDetails> itemDetailDictionary = new Dictionary<int,ItemDetails>();

    protected override void Awake()
    {
        base.Awake();
        // ��ʼ����Ʒ����
        InitItemDetailDictionary();

        // ������ҿ���嵥 
        CreateInventoryLists();
    }

    /// <summary>
    /// ��ʼ����Ʒ�����ֵ�
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
    /// ���ݿ�����ͻ�ȡ�����Ʒ�嵥
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public List<InventoryItem> GetInventoryListByInventoryType(InventoryLocation location)
    {
        return inventoryLists[(int)location];
    }

    /// <summary>
    /// ���ݿ�����ͻ�ȡ���������С
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    public int GetInventoryCapacityByInventoryType(InventoryLocation location)
    {
        return inventoryListCapacityIntArray[(int)location];
    }

    /// <summary>
    /// ���ÿ��������С
    /// </summary>
    /// <param name="location"></param>
    /// <param name="capacity"></param>
    public void SetInventoryCapation(InventoryLocation location, int capacity)
    {
        inventoryListCapacityIntArray[(int)location] = capacity;
    }

    /// <summary>
    /// ������Ʒ�����ȡ��Ʒ����
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
    /// ������ҿ���嵥
    /// </summary>
    private void CreateInventoryLists()
    {
        // �����С���ݿ��λ���е�countֵ��������countΪö��ֵ�����һ��Ԫ�أ����count����ֵ���ǿ�����͵�����
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.count];

        for(int i = 0; i < (int)InventoryLocation.count; i++)
        {
            inventoryLists[i] = new List<InventoryItem>();
        }

        inventoryListCapacityIntArray = new int[(int)InventoryLocation.count];

        inventoryListCapacityIntArray[(int)InventoryLocation.player] = Settings.playerInitialInventoryCapacity;
    }

    /// <summary>
    /// �����Ʒ������У�����ɾ�������Ϸ����
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
    /// �����Ʒ�������
    /// </summary>
    /// <param name="location"></param>
    /// <param name="item"></param>
    public void AddItem(InventoryLocation location,Item item)
    {
        int itemCode = item.ItemCode;
        // ���ݿ��λ�û�ȡ��Ӧ����Ʒ�б�
        List<InventoryItem> inventories = inventoryLists[(int)location];
        // ������Ʒ������嵥�еõ���Ʒ�ڿ���е�λ��
        int itemPosition = FindItemInInventory(location, itemCode);

        if (itemPosition != -1)
        {
            // ������Ѿ��������Ʒ��ֻ��Ҫ��������
            AddItemAtPosition(inventories, itemCode, itemPosition);
        }
        else
        {
            // �����û�������Ʒ������Ʒ��ӵ������
            AddItemAtPosition(inventories, itemCode);
        }

        // ���Ϳ�������Ϣ
        EventHandler.CallInventoryUpdateEvent(location, inventoryLists[(int)location]);
    }

    /// <summary>
    /// ������Ʒ�����ָ�����λ�õ���Ʒ�嵥�л�ȡ��Ʒ���ڵ�λ��
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
    /// ��ָ�������Ʒ�嵥������µ���Ʒ
    /// </summary>
    /// <param name="inventories"></param>
    /// <param name="itemCode"></param>
    private void AddItemAtPosition(List<InventoryItem> inventories, int itemCode)
    {
        InventoryItem item = new InventoryItem();

        item.itemCode = itemCode;
        item.itemQuantity = 1;
        
        //todo: �����ٿ���ôʵ�ַ����һ����λ��
        inventories.Add(item);


        DebugShowInventoryList(inventories);
    }

    /// <summary>
    /// ��ָ�������Ʒ�嵥�и�����Ʒ������
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
    /// ɾ����Ʒ
    /// </summary>
    /// <param name="inventoryLocation"></param>
    /// <param name="itemCode"></param>
    public void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        // ��ȡ��Ӧ�����Ʒ
        List<InventoryItem> inventoryItems = inventoryLists[(int)inventoryLocation];
        // ��ȡ��Ʒ���ڵ�λ��
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);
        if(itemPosition != -1)
        {
            RemoveItemAtPosition(inventoryItems,itemCode,itemPosition);
        }
        // ����UI��ʾ
        EventHandler.CallShowInventoryEvent(inventoryLocation, inventoryItems);

    }

    /// <summary>
    /// ɾ��ָ��λ������
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
            // ������������
            item.itemQuantity = quantity;
            item.itemCode = itemCode;
            inventoryItems[itemPosition] = item;
        }
        else
        {
            // ��������Ϊ0��ɾ��������������
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
        Debug.Log("------------------ ������Ϣ��ӡ���� ------------------");
    }

}
