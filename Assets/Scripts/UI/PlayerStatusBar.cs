using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ui控制  -  左上角player相关状态信息
/// </summary>
public class PlayerStatusBar : MonoBehaviour
{
    /// <summary>
    /// 血量
    /// </summary>
    public Image healthImage;
    /// <summary>
    /// 当前血量末端
    /// </summary>
    public Image healthCurrentEndImage;
    /// <summary>
    /// 精力
    /// </summary>
    public Image energyImage;
    /// <summary>
    /// 当前精力末端
    /// </summary>
    public Image energyCurrentStartImage;
    /// <summary>
    /// 玩家等级
    /// </summary>
    public TextMeshProUGUI levelText;
    /// <summary>
    /// 持有金币数量
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
        // 血量末端会随着血量百分比降低左移，血量百分比增加右移
        Vector3 healthCurrentEndLocalPosition = healthCurrentEndImage.rectTransform.localPosition;
        healthCurrentEndImage.rectTransform.localPosition = new Vector3(healthBarWidth * healthImage.fillAmount, healthCurrentEndLocalPosition.y);
    }

    /// <summary>
    /// 接收血量变化  
    /// </summary>
    /// <param name="persentage">血量百分比   current/max</param>
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount = persentage;
    }

    /// <summary>
    /// 玩家等级变化
    /// </summary>
    /// <param name="level"></param>
    public void SetPlayerLevel(int level)
    {
        levelText.text = level.ToString();  
    }

    /// <summary>
    /// 玩家金币变化
    /// </summary>
    /// <param name="coin"></param>
    public void SetPlayerCoin(int coin)
    {
        goldCoinText.text = coin.ToString();
    }
}
