using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemAttributeValue : MonoBehaviour
{
    [SerializeField] private Image icon = null;
    [SerializeField] private TextMeshProUGUI valueText = null;

    public void SetItemAttribute(Sprite sprite, string text)
    {
        icon.sprite = sprite;
        valueText.text = text;
    }

    
}
