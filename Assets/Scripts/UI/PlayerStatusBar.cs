using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ui����  -  ���Ͻ�player���״̬��Ϣ
/// </summary>
public class PlayerStatusBar : MonoBehaviour
{
    /// <summary>
    /// Ѫ��
    /// </summary>
    public Image healthImage;
    /// <summary>
    /// ��ǰѪ��ĩ��
    /// </summary>
    public Image healthCurrentEndImage;
    /// <summary>
    /// ����
    /// </summary>
    public Image energyImage;
    /// <summary>
    /// ��ǰ����ĩ��
    /// </summary>
    public Image energyCurrentStartImage;
    /// <summary>
    /// ��ҵȼ�
    /// </summary>
    public TextMeshProUGUI levelText;
    /// <summary>
    /// ���н������
    /// </summary>
    public TextMeshProUGUI goldCoinText;

    private float healthBarWidth;

    private void Start()
    {
        healthBarWidth = healthImage.rectTransform.rect.width;
        healthCurrentEndImage.rectTransform.localPosition = new Vector3(healthBarWidth * healthImage.fillAmount, healthCurrentEndImage.rectTransform.localPosition.y);
        levelText.text = "0";
        goldCoinText.text = "0";
    }

    private void Update()
    {
        // Ѫ��ĩ�˻�����Ѫ���ٷֱȽ������ƣ�Ѫ���ٷֱ���������
        Vector3 healthCurrentEndLocalPosition = healthCurrentEndImage.rectTransform.localPosition;
        healthCurrentEndImage.rectTransform.localPosition = new Vector3(healthBarWidth * healthImage.fillAmount, healthCurrentEndLocalPosition.y);
    }

    /// <summary>
    /// ����Ѫ���仯  
    /// </summary>
    /// <param name="persentage">Ѫ���ٷֱ�   current/max</param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }

    /// <summary>
    /// ��ҵȼ��仯
    /// </summary>
    /// <param name="level"></param>
    public void SetPlayerLevel(int level)
    {
        levelText.text = level.ToString();  
    }

    /// <summary>
    /// ��ҽ�ұ仯
    /// </summary>
    /// <param name="coin"></param>
    public void SetPlayerCoin(int coin)
    {
        goldCoinText.text = coin.ToString();
    }
}
