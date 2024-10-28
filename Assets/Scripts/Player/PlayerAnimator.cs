using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// player角色动画控制
/// </summary>
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private PlayerController playerController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        EventHandler.OnPlayerAttack += PlayerAttack;
        EventHandler.OnPlayerDash += PlayerDash;
    }

    private void OnDisable()
    {
        EventHandler.OnPlayerAttack -= PlayerAttack;
        EventHandler.OnPlayerDash -= PlayerDash;
    }

    private void Update()
    {
        SetAnimation();
    }


    public void SetAnimation()
    {
        anim.SetFloat(Settings.speedX, Mathf.Abs(rb.velocity.x));
        anim.SetFloat(Settings.velocityY, rb.velocity.y);
        anim.SetBool(Settings.isGround, physicsCheck.isGround);
        anim.SetBool(Settings.isDead, playerController.isDead);
        anim.SetBool(Settings.isAttack,playerController.isAttack);
        anim.SetBool(Settings.isRun, playerController.isRun);

    }

    /// <summary>
    /// player受伤   在inspector中绑定了对应的事件
    /// </summary>
    public void PlayerGetHurt()
    {
        anim.SetTrigger(Settings.hurt);
    }
    /// <summary>
    /// player攻击
    /// </summary>
    public void PlayerAttack()
    {
        anim.SetTrigger(Settings.attack);
    }

    /// <summary>
    /// player闪现
    /// </summary>
    public void PlayerDash()
    {
        anim.SetTrigger(Settings.isDash);
    }
}
