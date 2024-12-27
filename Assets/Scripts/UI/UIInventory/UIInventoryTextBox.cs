using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TMPro;
using UnityEngine;

public class UIInventoryTextBox : MonoBehaviour
{
    [Header("显示的文本")]
    /// <summary>
    /// 物品名称
    /// </summary>
    [SerializeField] private TextMeshProUGUI itemNameText = null;
    /// <summary>
    /// 物品类型
    /// </summary>
    [SerializeField] private TextMeshProUGUI itemTypeText = null;
    /// <summary>
    /// 物品描述
    /// </summary>
    [SerializeField] private TextMeshProUGUI itemDescriotionText = null;
    /// <summary>
    /// 显示物品属性内容列表  用于展示itemAttributeValue对应的物体实例
    /// </summary>
    public GameObject itemAttributeValueList = null;
    /// <summary>
    /// 显示物品属性内容预制体
    /// </summary>
    public GameObject itemAttributeValuePrefab;

    /// <summary>
    /// 显示物品描述信息
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="itemType"></param>
    /// <param name="itemDescription"></param>
    /// <param name="itemCode"></param>
    public void ShowInventoryTextBox(ItemDetails item)
    {
        string itemTypeText = InventoryManager.Instance.GetItemTypeDescriptionText(item.itemType);
        
        SetTextboxText(item.itemDescription, itemTypeText, item.itemLongDescription);

        if(item.itemValuesList != null )
        {
            CreateItemAttributeTextList(item.itemValuesList);
        }
        
    }

    /// <summary>
    /// 设置文本内容
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="itemType"></param>
    /// <param name="itemDescription"></param>
    private void SetTextboxText(string itemName,string itemType,string itemDescription)
    {
        itemNameText.text = itemName;
        itemTypeText.text = itemType;
        itemDescriotionText.text = itemDescription;

        
    }

    /// <summary>
    /// 生成物品属性信息
    /// </summary>
    /// <param name="itemCode"></param>
    private void CreateItemAttributeTextList(List<ItemValues> itemValues)
    {
        foreach(ItemValues itemValue in itemValues)
        {
            GameObject attributeInstance = Instantiate(itemAttributeValuePrefab, itemAttributeValueList.transform);
            if(attributeInstance != null)
            {
                UIItemAttributeValue uIItemAttributeValue = attributeInstance.GetComponent<UIItemAttributeValue>();
                // 获取属性图标
                Sprite attributeIcon = InventoryManager.Instance.GetItemAttributeIcon(itemValue.valueType);
                if(attributeIcon != null)
                {
                    // 设置显示的内容
                    uIItemAttributeValue.SetItemAttribute(attributeIcon,TransformItemValueToFormatByValueType(itemValue));
                }
                else
                {
                    Debug.Log("未获取到属性图标! 请重新检查配置信息! valueType = " + itemValue.valueType);
                }
            }
        }

    }

    /// <summary>
    /// 对配置的属性数值格式化显示
    /// </summary>
    /// <param name="itemvalue"></param>
    /// <returns></returns>
    private string TransformItemValueToFormatByValueType(ItemValues itemvalue)
    {
        StringBuilder text = new StringBuilder();
        switch(itemvalue.valueType)
        {
            case ItemValuesType.Duration:
                text.Append(FormatDurationText(itemvalue.value));
                break;
            case ItemValuesType.Health:
            case ItemValuesType.Energy:
            case ItemValuesType.AttackPoint:
            case ItemValuesType.MoveSpeed:
                // 正数需要在开头添加+号
                if (itemvalue.value > 0)
                {
                    text.Append("+ ");
                }
                // 百分比需要转换格式
                if (itemvalue.isPercentage)
                {
                    text.Append((itemvalue.value * 100).ToString("F1") + "%");
                }
                break;
            default:
                break;

        }
        return text.ToString();
    }

    /// <summary>
    /// 格式化时间
    /// </summary>
    /// <param name="duration">时间一定配置为整数  单位：秒</param>
    /// <returns></returns>
    private string FormatDurationText(float duration)
    {
        int totalSeconds = (int) duration;
        int minute = totalSeconds / 60;
        int second = totalSeconds % 60;

        // 确保格式，总是显示两位数，例如10:02
        return $"{minute:D2} : {second:D2}";
    }
    
}
