using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �˵�������ǩ
/// </summary>
public class MenuTopBar : MonoBehaviour
{
    /// <summary>
    /// ��ǰҳ��Ĳ˵�����
    /// </summary>
    public MenuType menuType;
    /// <summary>
    /// ��Ӧ�Ĳ˵�ҳ��
    /// </summary>
    public GameObject menuContent;


    public void OpenMenuContent()
    {

    }

    public void DebugWhenPress()
    {
        Debug.Log("���°�ť �� " + menuType);
    }
}
