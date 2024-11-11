using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������
/// </summary>
public class InventoryManager : SingletonMonoBehvior<InventoryManager>
{
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
        InitItemDetailDictionary();
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
    /// ������Ʒ�����ȡ��Ʒ����
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
