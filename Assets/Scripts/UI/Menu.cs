using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// ��ʼ��Ϸ��ť
    /// </summary>
    public GameObject newGameButton;

    private Button newGame;

    private void Start()
    {
        // ��ί���¼�
        newGame = newGameButton.GetComponent<Button>();
        newGame.onClick.AddListener(OnNewGameButtonClick);
    }

    private void OnEnable()
    {
        // ����EventSystemѡ�е����壬���������������ѡ��
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    private void OnDestroy()
    {
        // �Ƴ�ί�б����ڴ�й©
        if(newGame != null)
        {
            newGame.onClick.RemoveListener(OnNewGameButtonClick);
        }
    }

    /// <summary>
    /// �����ʼ��Ϸ
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
