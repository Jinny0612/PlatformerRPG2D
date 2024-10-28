using System.Collections;
using System.Collections.Generic;
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

    private float healthBarWidth;

    private void Start()
    {
        healthBarWidth = healthImage.rectTransform.rect.width;
        healthCurrentEndImage.rectTransform.localPosition = new Vector3(healthBarWidth * healthImage.fillAmount, healthCurrentEndImage.rectTransform.localPosition.y);
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
}
