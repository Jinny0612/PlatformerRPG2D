using TMPro;
using UnityEngine;

/// <summary>
/// ��Ʒ�������
/// </summary>
public class ItemNotification : MonoBehaviour
{
    /// <summary>
    /// ��ʾ��õ�����
    /// </summary>
    public TextMeshProUGUI numText;
    /// <summary>
    /// ��ǰ��ʾ������
    /// </summary>
    private int currentNum;
    /// <summary>
    /// ��ǰ��Ҫ��ʾ����ƷͼƬ
    /// </summary>
    public SpriteRenderer itemSpriteRenderer;
    /// <summary>
    /// ��ʾ����Ʒ����
    /// </summary>
    public TextMeshProUGUI itemDescription;
    /// <summary>
    /// ��õ���Ʒ���
    /// </summary>
    public int itemCode;
    /// <summary>
    /// ��������
    /// </summary>
    public NotificationType notificationType;


    /// <summary>
    /// ������ʾ����Ϣ
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
