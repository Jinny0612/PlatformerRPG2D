using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class BackpackUI : MonoBehaviour
{
    /// <summary>
    /// ͸��ͼƬ
    /// </summary>
    [SerializeField] private Sprite blankSprite;
    /// <summary>
    /// �������
    /// </summary>
    [SerializeField] private List<InventorySlot> inventorySlotList;
    /// <summary>
    /// ��ק����Ʒ
    /// </summary>
    public GameObject inventoryDraggedItem;
    /// <summary>
    /// ��Ʒ�����ı���
    /// </summary>
    public GameObject inventoryTextBoxGameobject;

    /// <summary>
    /// ��ǰ�������Ʒ������(ʹ���˶��ٸ񱳰�)
    /// </summary>
    private int curInventoryItemCount;
    /// <summary>
    /// ��ǰѡ�еĸ���
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
    /// ��տ����
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
    /// ��ʾ���UI
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
