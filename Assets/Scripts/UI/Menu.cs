using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// 开始游戏按钮
    /// </summary>
    public GameObject newGameButton;

    private void OnEnable()
    {
        // 设置EventSystem选中的物体，方便后续键盘上下选择
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    public void ExitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
