using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// 可与player交互物体头顶标识控制管理
/// </summary>
public class HeadIconController : MonoBehaviour
{
    /// <summary>
    /// 标志图片物体
    /// </summary>
    public GameObject signSprite;
    /// <summary>
    /// 动画
    /// </summary>
    public Animator animator;
    /// <summary>
    /// 是否显示  true-显示
    /// </summary>
    public bool canOpen;
    /// <summary>
    /// 可互动物体
    /// </summary>
    public IInteractable interactable;

    private PlayerInputControls playerInput;

    private void Awake()
    {
        // 获取对应游戏物体身上的组件  因为启动游戏时signSprite是关闭的，所以需要指定对应物体
        animator = signSprite.GetComponent<Animator>();
        playerInput = new PlayerInputControls();
        

        // 绑定按键
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
            // 可互动物体 与 player
            canOpen = true;
            // 获取可互动物体
            interactable = GetComponent<IInteractable>();
            //Debug.Log(collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 退出触发器  重置为false
        canOpen = false;
    }

    /// <summary>
    /// 绑定的按键被按下触发的事件
    /// </summary>
    /// <param name="obj"></param>
    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if(canOpen)
        {
            //打开箱子
            interactable.TriggerAction();
            
            // 设置为不可见  取消碰撞体
            GetComponent<Collider2D>().enabled = false;
        }
    }

}
