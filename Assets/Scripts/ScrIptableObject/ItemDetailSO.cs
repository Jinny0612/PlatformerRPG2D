using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDetailSO", menuName = "ScriptableObjects/ObjectsInfo/ItemDetailSO")]
public class ItemDetailSO : ScriptableObject
{
    public List<ItemDetails> Details = new List<ItemDetails>();
}


