using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 物品信息
/// </summary>
public class Item : MonoBehaviour
{
    [ItemCodeDescription]
    [SerializeField]
    private int _itemCode;

    public int ItemCode { get { return _itemCode; } set { _itemCode = value; } }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


}
