using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    /// <summary>
    /// ��ʼ��Ϸ��ť
    /// </summary>
    public GameObject newGameButton;

    private void OnEnable()
    {
        // ����EventSystemѡ�е����壬���������������ѡ��
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    public void ExitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
