using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �������
/// </summary>
public class Chest : MonoBehaviour,IInteractable
{
    public UnityEvent openChest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �򿪲���
    /// </summary>
    public void TriggerAction()
    {
        //todo: �����ӡ�������������ui��ʾ��Ʒ
        openChest?.Invoke();
    }
}
