using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// �Զ������Ի���������Inspector����ʾ�ض�����,����Ŀ�Ӽ�飬����¼����Ʒ�������
/// </summary>
[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    private ItemDetailSO itemDetailSO;

    /// <summary>
    /// �������Ը߶�
    /// ��Ϊ�Ǹ�����Ʒ������ʾ��Ʒ������������Ҫ�����ĸ߶�
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) *2;
    }

    

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // todo:���﷢���������list������ʾ�ģ���ôǰ���itemcode��itemDescriptionλ�ò��ԣ���Ҫ���������һ��
        //ȷ���б��б���һ�¶���
        position = EditorGUI.IndentedRect(position);
        
        //ʹ��beginproperty / endproperty ȷ�������߼���������������
        EditorGUI.BeginProperty(position, label, property);//��ʼ��������
        //������Ʒ����
        var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);
        //������Ʒ����
        EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description",
            GetItemDescription(property.intValue));

        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = newValue;
        }

        EditorGUI.EndProperty();//��������
    }

    /// <summary>
    /// ��ȡ��Ʒ����
    /// </summary>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    private string GetItemDescription(int itemCode)
    {
        // ����·������asset��Դ
        if (itemDetailSO == null)
        {
            itemDetailSO = AssetDatabase.LoadAssetAtPath("Assets/DataSO/ObjectsInfoSO/ItemDetailSO.asset",typeof(ItemDetailSO)) as ItemDetailSO;
        }
        if(itemDetailSO == null)
        {
            Debug.Log("δ��ȡ��ItemDetailSO�����ݣ������Ƿ�����");
        }
        List<ItemDetails> itemDetailList = itemDetailSO.Details;
        ItemDetails itemDetail = itemDetailList.Find(i => itemCode == i.itemCode);
        if (itemDetail != null)
        {
            return itemDetail.itemDescription;
        }
        return null;
    }
}
