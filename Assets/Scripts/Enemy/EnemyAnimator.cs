using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˶������ƻ���ű�
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
    /// ��������  ��inspector�а��¼�
    /// </summary>
    public void EnemyHurt()
    {
        anim.SetTrigger(Settings.enemyHurt);
    }

    /// <summary>
    /// ��ս����
    /// </summary>
    public void EnemyMeleeAttack()
    {
        anim.SetTrigger(Settings.enemyMelee);
    }

    /// <summary>
    /// Զ�̹���
    /// </summary>
    public void EnemyRemotedAttack()
    {
        anim.SetTrigger(Settings.enemyRemoted);
    }

    /// <summary>
    /// ������
    /// </summary>
    public void EnemyAwake()
    {
        anim.SetTrigger(Settings.enemyAwake);
    }

}
