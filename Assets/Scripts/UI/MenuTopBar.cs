using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 菜单顶部标签
/// </summary>
public class MenuTopBar : MonoBehaviour
{
    /// <summary>
    /// 当前页面的菜单类型
    /// </summary>
    public MenuType menuType;
    /// <summary>
    /// 对应的菜单页面
    /// </summary>
    public GameObject menuContent;


    public void OpenMenuContent()
    {

    }

    public void DebugWhenPress()
    {
        Debug.Log("按下按钮 ： " + menuType);
    }
}
