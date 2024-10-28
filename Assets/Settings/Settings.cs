using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 公共参数设置
/// </summary>
public static class Settings 
{
    #region Player动画参数

    /// <summary>
    /// x方向的角色速度大小，无方向
    /// </summary>
    public static int speedX;
    /// <summary>
    /// y方向的角色速度，有方向
    /// </summary>
    public static int velocityY;
    /// <summary>
    /// 角色是否在地面上
    /// </summary>
    public static int isGround;
    /// <summary>
    /// 角色受伤
    /// </summary>
    public static int hurt;
    /// <summary>
    /// 角色是否死亡
    /// </summary>
    public static int isDead;
    /// <summary>
    /// 是否攻击
    /// </summary>
    public static int isAttack;
    /// <summary>
    /// 触发单次攻击
    /// </summary>
    public static int attack;
    /// <summary>
    /// 是否跑步
    /// </summary>
    public static int isRun;
    /// <summary>
    /// 是否闪现
    /// </summary>
    public static int isDash;

    #endregion

    #region Enemy公共动画参数  所有enemy都需要添加以下相同的参数

    public static int enemyWalk;
    public static int enemyRun;
    public static int enemyHurt;
    public static int enemyDeath;
    public static int enemyMelee;
    public static int enemyRemoted;
    public static int enemyAwake;

    #endregion

    #region 可互动标识动画参数

    public static int canOpen;
    public static int canCheck;
    public static int canLock;
    public static int canUnlock;

    #endregion

    #region  可互动物品动用动画参数

    public static int open;

    #endregion

    static Settings()
    {
        // 与Animator窗口中的parameter绑定
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

        #region 可互动标识

        canOpen = Animator.StringToHash("canOpen");
        canCheck = Animator.StringToHash("canCheck");
        canLock = Animator.StringToHash("canLock");
        canUnlock = Animator.StringToHash("canUnlock");

        #endregion

        #region  可互动物品动用动画参数

        open = Animator.StringToHash("open");

        #endregion
}
}
