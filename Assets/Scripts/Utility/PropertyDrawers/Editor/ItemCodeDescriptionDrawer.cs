using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// 自定义属性绘制器，在Inspector上显示特定属性,便于目视检查，避免录入物品代码错误
/// </summary>
[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    private ItemDetailSO itemDetailSO;

    /// <summary>
    /// 设置属性高度
    /// 因为是根据物品代码显示物品描述，所以需要两倍的高度
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) *2;
    }

    

    /// <summary>
    /// 绘制属性
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // todo:这里发现如果是在list下面显示的，那么前面的itemcode和itemDescription位置不对，需要往后面调整一下
        //确保列表中保持一致对齐
        position = EditorGUI.IndentedRect(position);
        
        //使用beginproperty / endproperty 确保绘制逻辑适用于整个属性
        EditorGUI.BeginProperty(position, label, property);//开始绘制属性
        //绘制物品代码
        var newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);
        //绘制物品描述
        EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description",
            GetItemDescription(property.intValue));

        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = newValue;
        }

        EditorGUI.EndProperty();//结束绘制
    }

    /// <summary>
    /// 获取物品描述
    /// </summary>
    /// <param name="itemCode"></param>
    /// <returns></returns>
    private string GetItemDescription(int itemCode)
    {
        // 根据路径加载asset资源
        if (itemDetailSO == null)
        {
            itemDetailSO = AssetDatabase.LoadAssetAtPath("Assets/DataSO/ObjectsInfoSO/ItemDetailSO.asset",typeof(ItemDetailSO)) as ItemDetailSO;
        }
        if(itemDetailSO == null)
        {
            Debug.Log("未获取到ItemDetailSO的数据，请检查是否配置");
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
