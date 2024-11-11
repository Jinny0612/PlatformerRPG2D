using TMPro;
using UnityEngine;

/// <summary>
/// 物品获得提醒
/// </summary>
public class ItemNotification : MonoBehaviour
{
    /// <summary>
    /// 显示获得的数量
    /// </summary>
    public TextMeshProUGUI numText;
    /// <summary>
    /// 当前显示的数量
    /// </summary>
    private int currentNum;
    /// <summary>
    /// 当前需要显示的物品图片
    /// </summary>
    public SpriteRenderer itemSpriteRenderer;
    /// <summary>
    /// 显示的物品描述
    /// </summary>
    public TextMeshProUGUI itemDescription;
    /// <summary>
    /// 获得的物品编号
    /// </summary>
    public int itemCode;
    /// <summary>
    /// 提醒类型
    /// </summary>
    public NotificationType notificationType;


    /// <summary>
    /// 设置显示的信息
    /// </summary>
    /// <param name="num"></param>
    public void SetItemNotification(int num,Sprite itemSprite,string itemDescription)
    {
        currentNum += num;
        numText.text = currentNum.ToString();

        if (itemSprite != null && itemSpriteRenderer != null)
        {
            itemSpriteRenderer.sprite = itemSprite;
            this.itemDescription.text = itemDescription;
        }
    }
}
