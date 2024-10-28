using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������������
/// </summary>
public static class Settings 
{
    #region Player��������

    /// <summary>
    /// x����Ľ�ɫ�ٶȴ�С���޷���
    /// </summary>
    public static int speedX;
    /// <summary>
    /// y����Ľ�ɫ�ٶȣ��з���
    /// </summary>
    public static int velocityY;
    /// <summary>
    /// ��ɫ�Ƿ��ڵ�����
    /// </summary>
    public static int isGround;
    /// <summary>
    /// ��ɫ����
    /// </summary>
    public static int hurt;
    /// <summary>
    /// ��ɫ�Ƿ�����
    /// </summary>
    public static int isDead;
    /// <summary>
    /// �Ƿ񹥻�
    /// </summary>
    public static int isAttack;
    /// <summary>
    /// �������ι���
    /// </summary>
    public static int attack;
    /// <summary>
    /// �Ƿ��ܲ�
    /// </summary>
    public static int isRun;
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    public static int isDash;

    #endregion

    #region Enemy������������  ����enemy����Ҫ���������ͬ�Ĳ���

    public static int enemyWalk;
    public static int enemyRun;
    public static int enemyHurt;
    public static int enemyDeath;
    public static int enemyMelee;
    public static int enemyRemoted;
    public static int enemyAwake;

    #endregion

    #region �ɻ�����ʶ��������

    public static int canOpen;
    public static int canCheck;
    public static int canLock;
    public static int canUnlock;

    #endregion

    #region  �ɻ�����Ʒ���ö�������

    public static int open;

    #endregion

    static Settings()
    {
        // ��Animator�����е�parameter��
        #region Player
        speedX = Animator.StringToHash("speedX");
        velocityY = Animator.StringToHash("velocityY");
        isGround = Animator.StringToHash("isGround");
        hurt = Animator.StringToHash("hurt");
        isDead = Animator.StringToHash("isDead");
        isAttack = Animator.StringToHash("isAttack");
        attack = Animator.StringToHash("attack");
        isRun = Animator.StringToHash("isRun");
        isDash = Animator.StringToHash("dash");
        #endregion

        #region Enemy
        enemyWalk = Animator.StringToHash("walk");
        enemyRun = Animator.StringToHash("run");
        enemyHurt = Animator.StringToHash("hurt");
        enemyDeath = Animator.StringToHash("dead");
        enemyMelee = Animator.StringToHash("melee");
        enemyRemoted = Animator.StringToHash("remoted");
        enemyAwake = Animator.StringToHash("awake");
        #endregion

        #region �ɻ�����ʶ

        canOpen = Animator.StringToHash("canOpen");
        canCheck = Animator.StringToHash("canCheck");
        canLock = Animator.StringToHash("canLock");
        canUnlock = Animator.StringToHash("canUnlock");

        #endregion

        #region  �ɻ�����Ʒ���ö�������

        open = Animator.StringToHash("open");

        #endregion
}
}
