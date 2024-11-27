using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��Ʒ����
/// ��Ҫ������ScriptableObjects������
/// </summary>
[System.Serializable]
public class ItemDetails
{
    /// <summary>
    /// ��Ʒ����  Ψһ
    /// </summary>
    public int itemCode;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public ItemType itemType;
    /// <summary>
    /// ��Ʒ���
    /// </summary>
    public string itemDescription;
    /// <summary>
    /// ��ƷͼƬ
    /// </summary>
    public Sprite itemSprite;
    /// <summary>
    /// Ԥ����
    /// </summary>
    public GameObject prefab;
    /// <summary>
    /// ��Ʒ��ϸ����
    /// </summary>
    public string itemLongDescription;
    /// <summary>
    /// �Ƿ��ʰȡ
    /// </summary>
    public bool isPickable;
    /// <summary>
    /// �Ƿ�ɶ���
    /// </summary>
    public bool canBeDropped;
    /// <summary>
    /// ��Ʒ��ֵ�����б�
    /// </summary>
    public List<ItemValues> itemValuesList;
}

/// <summary>
/// ��Ʒ��ֵ����
/// </summary>
[System.Serializable]
public class ItemValues
{
    /// <summary>
    /// ��ֵ����
    /// </summary>
    public ItemValuesType valueType;
    /// <summary>
    /// ��ֵ
    /// </summary>
    public float value;
    /// <summary>
    /// �Ƿ�ٷֱȣ�����value��Χ0-1
    /// </summary>
    public bool isPercentage;
}
