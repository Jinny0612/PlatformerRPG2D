using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// ����player��������ͷ����ʶ���ƹ���
/// </summary>
public class HeadIconController : MonoBehaviour
{
    /// <summary>
    /// ��־ͼƬ����
    /// </summary>
    public GameObject signSprite;
    /// <summary>
    /// ����
    /// </summary>
    public Animator animator;
    /// <summary>
    /// �Ƿ���ʾ  true-��ʾ
    /// </summary>
    public bool canOpen;
    /// <summary>
    /// �ɻ�������
    /// </summary>
    public IInteractable interactable;

    private PlayerInputControls playerInput;

    private void Awake()
    {
        // ��ȡ��Ӧ��Ϸ�������ϵ����  ��Ϊ������ϷʱsignSprite�ǹرյģ�������Ҫָ����Ӧ����
        animator = signSprite.GetComponent<Animator>();
        playerInput = new PlayerInputControls();
        

        // �󶨰���
        playerInput.GamePlay.Confirm.started += OnConfirm;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    private void Update()
    {
        signSprite.SetActive(canOpen);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(Tags.PLAYER) && this.CompareTag(Tags.INTERACTIVE))
        {
            // �ɻ������� �� player
            canOpen = true;
            // ��ȡ�ɻ�������
            interactable = GetComponent<IInteractable>();
            //Debug.Log(collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �˳�������  ����Ϊfalse
        canOpen = false;
    }

    /// <summary>
    /// �󶨵İ��������´������¼�
    /// </summary>
    /// <param name="obj"></param>
    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if(canOpen)
        {
            //������
            interactable.TriggerAction();
            
            // ����Ϊ���ɼ�  ȡ����ײ��
            GetComponent<Collider2D>().enabled = false;
        }
    }

}
