using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����л����������͵�
/// </summary>
public class Teleport : MonoBehaviour,IInteractable
{
    /// <summary>
    /// �³����Ľ�ɫ����
    /// </summary>
    public Vector3 positionToGo;
    /// <summary>
    /// �³���
    /// </summary>
    public GameSceneSO sceneToGo;
    /// <summary>
    /// ���������¼�
    /// </summary>
    public SceneLoadEventSO loadEventSO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ���������л���Ϊ
    /// </summary>
    public void TriggerAction()
    {
        //Debug.Log("try to teleport");
        //Debug.Log(loadSceneEvent);
        // ���ó��������¼�
        //loadEventSO.CallLoadRequestEvent(sceneToGo, positionToGo, true);

        EventHandler.CallSceneLoadEvent(sceneToGo,positionToGo, true);
    }
}
