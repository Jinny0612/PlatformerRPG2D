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
    /// ��Ʒ��ϸ����
    /// </summary>
    public string itemLongDescription;
}
