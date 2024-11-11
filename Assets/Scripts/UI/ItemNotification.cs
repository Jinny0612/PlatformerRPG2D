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
    /// 提醒显示时长
    /// </summary>
    private float visiableTime;
    /// <summary>
    /// 提醒显示时长倒计时
    /// </summary>
    private float visiableTimeCounter;

    private void Awake()
    {
        visiableTime = 3f;
        visiableTimeCounter = visiableTime;
    }

    private void Update()
    {
        TimeCounter();
    }

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

        //重置显示时长
        visiableTimeCounter = visiableTime;
    }

    /// <summary>
    /// 计时器
    /// </summary>
    private void TimeCounter()
    {
        if(visiableTimeCounter <= 0)
        {
            // 倒计时结束，不可见，删除这个通知，从队列中也删除
            Destroy(gameObject);
            EventHandler.OnDeleteNotificationAfterTimeCount(gameObject);
        }
        else
        {
            visiableTimeCounter -= Time.deltaTime;
        }
    }
}
