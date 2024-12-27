using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��Ʒ����ͼ��ӳ��
/// attention: ���õ�˳�������ItemValuesTypeö�����е�����˳��һ��
/// </summary>
[CreateAssetMenu(fileName = "ItemAttributeIconMappingSO", menuName = "ScriptableObjects/UI/ItemAttributeIconMappingSO")]
public class ItemAttributeIconMappingSO : ScriptableObject
{
    [System.Serializable]
    public struct AttributeIcon
    {
        /// <summary>
        /// ��ֵ����
        /// </summary>
        public ItemValuesType valueType;
        /// <summary>
        /// ��ֵ���Ͷ�Ӧ��ͼ��
        /// </summary>
        public Sprite icon;
    }

    /// <summary>
    /// �����б�
    /// </summary>
    public AttributeIcon[] attributeIcons;

}


