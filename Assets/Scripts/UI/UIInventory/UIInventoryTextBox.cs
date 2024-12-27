using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using TMPro;
using UnityEngine;

public class UIInventoryTextBox : MonoBehaviour
{
    [Header("��ʾ���ı�")]
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    [SerializeField] private TextMeshProUGUI itemNameText = null;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    [SerializeField] private TextMeshProUGUI itemTypeText = null;
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    [SerializeField] private TextMeshProUGUI itemDescriotionText = null;
    /// <summary>
    /// ��ʾ��Ʒ���������б�  ����չʾitemAttributeValue��Ӧ������ʵ��
    /// </summary>
    public GameObject itemAttributeValueList = null;
    /// <summary>
    /// ��ʾ��Ʒ��������Ԥ����
    /// </summary>
    public GameObject itemAttributeValuePrefab;

    /// <summary>
    /// ��ʾ��Ʒ������Ϣ
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
    /// �����ı�����
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
    /// ������Ʒ������Ϣ
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
                // ��ȡ����ͼ��
                Sprite attributeIcon = InventoryManager.Instance.GetItemAttributeIcon(itemValue.valueType);
                if(attributeIcon != null)
                {
                    // ������ʾ������
                    uIItemAttributeValue.SetItemAttribute(attributeIcon,TransformItemValueToFormatByValueType(itemValue));
                }
                else
                {
                    Debug.Log("δ��ȡ������ͼ��! �����¼��������Ϣ! valueType = " + itemValue.valueType);
                }
            }
        }

    }

    /// <summary>
    /// �����õ�������ֵ��ʽ����ʾ
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
                // ������Ҫ�ڿ�ͷ���+��
                if (itemvalue.value > 0)
                {
                    text.Append("+ ");
                }
                // �ٷֱ���Ҫת����ʽ
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
    /// ��ʽ��ʱ��
    /// </summary>
    /// <param name="duration">ʱ��һ������Ϊ����  ��λ����</param>
    /// <returns></returns>
    private string FormatDurationText(float duration)
    {
        int totalSeconds = (int) duration;
        int minute = totalSeconds / 60;
        int second = totalSeconds % 60;

        // ȷ����ʽ��������ʾ��λ��������10:02
        return $"{minute:D2} : {second:D2}";
    }
    
}
