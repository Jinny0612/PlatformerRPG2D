using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人动画控制基类脚本
/// </summary>
[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    protected Animator anim;
    protected Enemy enemy;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        EventHandler.OnEnemyGetHurtEvent += EnemyGetHurtEvent;
    }

    private void OnDisable()
    {
        EventHandler.OnEnemyGetHurtEvent -= EnemyGetHurtEvent;
    }

    private void Update()
    {
        SetAnimation();
    }

    public void SetAnimation()
    {
        anim.SetBool(Settings.enemyWalk, !enemy.wait);
        anim.SetBool(Settings.enemyRun,enemy.isRun);
        anim.SetBool(Settings.enemyDeath, enemy.isDead);
        
    }

    private void EnemyGetHurtEvent(Transform transform)
    {
        EnemyHurt();
    }

    /// <summary>
    /// 敌人受伤  在inspector中绑定事件
    /// </summary>
    public void EnemyHurt()
    {
        anim.SetTrigger(Settings.enemyHurt);
    }

    /// <summary>
    /// 近战攻击
    /// </summary>
    public void EnemyMeleeAttack()
    {
        anim.SetTrigger(Settings.enemyMelee);
    }

    /// <summary>
    /// 远程攻击
    /// </summary>
    public void EnemyRemotedAttack()
    {
        anim.SetTrigger(Settings.enemyRemoted);
    }

    /// <summary>
    /// 被唤醒
    /// </summary>
    public void EnemyAwake()
    {
        anim.SetTrigger(Settings.enemyAwake);
    }

}
