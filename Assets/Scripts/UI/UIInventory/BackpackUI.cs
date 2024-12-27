using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    /// <summary>
    /// 透明图片
    /// </summary>
    [SerializeField] private Sprite blankSprite;
    /// <summary>
    /// 库存内容
    /// </summary>
    [SerializeField] private List<InventorySlot> inventorySlotList;
    /// <summary>
    /// 拖拽的物品
    /// </summary>
    public GameObject inventoryDraggedItem;
    /// <summary>
    /// 物品详情文本框
    /// </summary>
    public GameObject inventoryTextBoxGameobject;

    /// <summary>
    /// 当前库存中物品的数量(使用了多少格背包)
    /// </summary>
    private int curInventoryItemCount;
    /// <summary>
    /// 当前选中的格子
    /// </summary>
    private InventorySlot curSelectedSlot;



    private void Awake()
    {
        curInventoryItemCount = 0;
    }

    private void OnEnable()
    {
        EventHandler.OnInventoryUpdateEvent += ShowInventory;
    }

    private void OnDisable()
    {
        EventHandler.OnInventoryUpdateEvent -= ShowInventory;
    }

    /// <summary>
    /// 清空库存插槽
    /// </summary>
    private void ClearInventorySlots()
    {
        if(inventorySlotList.Count > 0)
        {
            foreach(InventorySlot slot in inventorySlotList)
            {
                slot.inventoryItemImage.sprite = blankSprite;
                slot.itemQuantityText.text = "";
                slot.itemDetails = null;
                slot.itemQuantity = 0;
            }
            curInventoryItemCount = 0;
        }
    }

    /// <summary>
    /// 显示库存UI
    /// </summary>
    /// <param name="location"></param>
    /// <param name="inventoryItems"></param>
    private void ShowInventory(InventoryLocation location, List<InventoryItem> inventoryItems)
    {
        if(location == InventoryLocation.player)
        {
            ClearInventorySlots();

            if(inventorySlotList.Count > 0 && inventoryItems.Count > 0)
            {
                for(int i = 0; i < inventorySlotList.Count; i++)
                {
                    if(i < inventoryItems.Count)
                    {
                        int itemCode = inventoryItems[i].itemCode;

                        ItemDetails itemDetail = InventoryManager.Instance.GetItemDetailByCode(itemCode); ;

                        if(itemDetail != null)
                        {
                            inventorySlotList[i].inventoryItemImage.sprite = itemDetail.itemSprite;
                            inventorySlotList[i].itemDetails = itemDetail;
                            inventorySlotList[i].itemQuantity = inventoryItems[i].itemQuantity;
                            inventorySlotList[i].itemQuantityText.text = inventorySlotList[i].itemQuantity.ToString();
                            inventorySlotList[i].SetSlotNumber(i);
                            curInventoryItemCount++;
                        }
                        
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
    
}
