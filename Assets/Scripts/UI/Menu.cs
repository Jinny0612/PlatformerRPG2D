using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// 开始游戏按钮
    /// </summary>
    public GameObject newGameButton;

    private Button newGame;

    private void Start()
    {
        // 绑定委托事件
        newGame = newGameButton.GetComponent<Button>();
        newGame.onClick.AddListener(OnNewGameButtonClick);
    }

    private void OnEnable()
    {
        // 设置EventSystem选中的物体，方便后续键盘上下选择
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    private void OnDestroy()
    {
        // 移除委托避免内存泄漏
        if(newGame != null)
        {
            newGame.onClick.RemoveListener(OnNewGameButtonClick);
        }
    }

    /// <summary>
    /// 点击开始游戏
    /// </summary>
    private void OnNewGameButtonClick()
    {
        EventHandler.CallNewGameEvent();
    }

    public void ExitGame()
    {

        Application.Quit();
    }
}
