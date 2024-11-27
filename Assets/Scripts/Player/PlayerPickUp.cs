using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ʰȡ�ű�
/// </summary>
public class PlayerPickUp : MonoBehaviour
{
    /// <summary>
    /// �ȴ�ʰȡ����Ʒ�б�
    /// </summary>
    private List<GameObject> pickableItemList = new List<GameObject>();

    [Header("ʰȡ��ز���")]
    [SerializeField]private CapsuleCollider2D capsuleCollider;
    /// <summary>
    /// ���ʰȡ��Χ
    /// </summary>
    [SerializeField] private Vector2 boxCastSize;
    /// <summary>
    /// ������
    /// </summary>
    [SerializeField] private float boxDistance;
    /// <summary>
    /// �������Ʒ����layer
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
    /// ����ʰȡ��Ʒ
    /// </summary>
    /// <returns></returns>
    private void FoundPickableItems()
    {
        // ʰȡ��Χ�����ĵ�
        Vector2 boxCastOrigin = capsuleCollider.bounds.center;
        // ��ⷶΧ�ڵ�����
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
                        // ��Ʒ��ʰȡ�Ҳ��ڴ�ʰȡ�б��У������б�
                        pickableItemList.Add(dropItem);
                    }
                }
            }
        }
    }

    /// <summary>
    /// ʰȡ��Ʒ
    /// </summary>
    private void PickUpItem()
    {
        List<GameObject> tempList = new List<GameObject>(pickableItemList);

        foreach (GameObject itemObject in tempList)
        {
            //todo: ��Ʒ��player�ƶ�,���Ǵ�ʱ���õ�ʰȡ��Χ��С��������ʱû������߼����������Կ�һ���Ƿ�����Ż�


            //todo: ��Ʒ����player����
            Item itemPicked = itemObject.GetComponent<Item>();
            InventoryManager.Instance.AddItem(InventoryLocation.player, itemPicked, itemObject);

            ItemDetails details = InventoryManager.Instance.GetItemDetailByCode(itemObject.GetComponent<Item>().ItemCode);
            //todo: �Ӵ�ʰȡ�б���ɾ�������Ʒ
            pickableItemList.Remove(itemObject);

            // ʰȡ����
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
