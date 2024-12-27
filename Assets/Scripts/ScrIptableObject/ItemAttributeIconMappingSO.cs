using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 物品属性图标映射
/// attention: 配置的顺序必须与ItemValuesType枚举类中的类型顺序一致
/// </summary>
[CreateAssetMenu(fileName = "ItemAttributeIconMappingSO", menuName = "ScriptableObjects/UI/ItemAttributeIconMappingSO")]
public class ItemAttributeIconMappingSO : ScriptableObject
{
    [System.Serializable]
    public struct AttributeIcon
    {
        /// <summary>
        /// 数值类型
        /// </summary>
        public ItemValuesType valueType;
        /// <summary>
        /// 数值类型对应的图标
        /// </summary>
        public Sprite icon;
    }

    /// <summary>
    /// 配置列表
    /// </summary>
    public AttributeIcon[] attributeIcons;

}


