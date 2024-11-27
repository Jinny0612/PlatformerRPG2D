using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家拾取脚本
/// </summary>
public class PlayerPickUp : MonoBehaviour
{
    /// <summary>
    /// 等待拾取的物品列表
    /// </summary>
    private List<GameObject> pickableItemList = new List<GameObject>();

    [Header("拾取相关参数")]
    [SerializeField]private CapsuleCollider2D capsuleCollider;
    /// <summary>
    /// 玩家拾取范围
    /// </summary>
    [SerializeField] private Vector2 boxCastSize;
    /// <summary>
    /// 检测距离
    /// </summary>
    [SerializeField] private float boxDistance;
    /// <summary>
    /// 掉落的物品所在layer
    /// </summary>
    [SerializeField] private LayerMask dropItemLayer;


    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        FoundPickableItems();
        if (pickableItemList.Count > 0)
        {
            PickUpItem();
        }
    }

    /// <summary>
    /// 检测可拾取物品
    /// </summary>
    /// <returns></returns>
    private void FoundPickableItems()
    {
        // 拾取范围的中心点
        Vector2 boxCastOrigin = capsuleCollider.bounds.center;
        // 检测范围内的物体
        RaycastHit2D[] hits = Physics2D.BoxCastAll(boxCastOrigin,boxCastSize,0f,Vector2.zero,boxDistance,dropItemLayer);
        
        if(hits != null && hits.Length > 0)
        {
            
            foreach (RaycastHit2D hit in hits)
            {
                GameObject dropItem = hit.collider.gameObject;
                Item item = dropItem.GetComponent<Item>();
                if(item != null)
                {
                    ItemDetails details = InventoryManager.Instance.GetItemDetailByCode(item.ItemCode);
                    if(details != null && details.isPickable && !pickableItemList.Contains(dropItem))
                    {
                        // 物品可拾取且不在待拾取列表中，加入列表
                        pickableItemList.Add(dropItem);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 拾取物品
    /// </summary>
    private void PickUpItem()
    {
        List<GameObject> tempList = new List<GameObject>(pickableItemList);

        foreach (GameObject itemObject in tempList)
        {
            //todo: 物品朝player移动,但是此时设置的拾取范围很小，所以暂时没有这个逻辑，后续可以看一下是否可以优化


            //todo: 物品进入player背包
            Item itemPicked = itemObject.GetComponent<Item>();
            InventoryManager.Instance.AddItem(InventoryLocation.player, itemPicked, itemObject);

            ItemDetails details = InventoryManager.Instance.GetItemDetailByCode(itemObject.GetComponent<Item>().ItemCode);
            //todo: 从待拾取列表中删除这个物品
            pickableItemList.Remove(itemObject);

            // 拾取提醒
            EventHandler.OnCreateNotificationEvent(NotificationType.Item, 1, itemPicked.ItemCode);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector2 boxCastOrigin = capsuleCollider.bounds.center;
        Gizmos.DrawWireCube(boxCastOrigin, boxCastSize);
    }

}
