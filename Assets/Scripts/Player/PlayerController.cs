using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// 角色控制脚本
/// </summary>
[RequireComponent(typeof(AudioDefinetion))]
public class PlayerController : SingletonMonoBehvior<PlayerController>
{

    [HideInInspector]public PlayerInputControls inputControl;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    private CapsuleCollider2D coll;
    private Character character;

    /// <summary>
    /// 输入的方向
    /// </summary>
    [HideInInspector]public Vector2 inputDirection;

    [Header("监听事件")]
    /// <summary>
    /// 场景加载事件   加载场景时禁用player的移动
    /// </summary>
    public SceneLoadEventSO loadEvent;
    public VoidEventSO afterSceneLoadedEvent;

    [Header("基本参数")]
    /// <summary>
    /// 移动速度
    /// </summary>
    [HideInInspector] public float currentSpeed;
    /// <summary>
    /// 跑步速度
    /// </summary>
    public float runSpeed;
    /// <summary>
    /// 行走速度
    /// </summary>
    public float walkSpeed;
    /// <summary>
    /// 闪现速度
    /// </summary>
    public float dashSpeed;
    /// <summary>
    /// 之前的速度  便于某些行为结束后恢复到初始状态
    /// </summary>
    private float lastSpeed;
    /// <summary>
    /// 跳跃时对角色施加一个推力
    /// </summary>
    public float jumpForce;
    /// <summary>
    /// 受伤时对角色施加一个受击方向的推力
    /// </summary>
    public float hurtForce;
    

    [Header("物理材质")]
    /// <summary>
    /// 普通材质 暂时地面使用，有摩擦力
    /// </summary>
    public PhysicsMaterial2D normal;
    /// <summary>
    /// 墙面材质  暂时跳跃使用，无摩擦力，碰墙可以直接滑落
    /// </summary>
    public PhysicsMaterial2D wall;

    [Header("角色状态")]
    /// <summary>
    /// 是否跑步
    /// </summary>
    public bool isRun;
    /// <summary>
    /// 是否受伤
    /// </summary>
    public bool isHurt;
    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool isDead;
    /// <summary>
    /// 是否攻击
    /// </summary>
    public bool isAttack;
    /// <summary>
    /// 是否闪现   无敌状态
    /// </summary>
    public bool isDash;

    public UnityEvent OnInvulnerableDash;
    

    #region 生命周期
    protected override void Awake()
    {
        base.Awake();
        inputControl = new PlayerInputControls();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        character = GetComponent<Character>();

        //默认走路
        isRun = false;
        currentSpeed = walkSpeed;

        //走路跑步切换
        inputControl.GamePlay.SwitchBetweenWalkAndRun.started += SwitchWalkAndRun;
        // 按下跳跃按键的瞬间触发事件Jump
        inputControl.GamePlay.Jump.started += Jump;
        // 攻击事件绑定
        inputControl.GamePlay.Fire.started += PlayerAttack;
        //inputControl.GamePlay.Dash.started += PlayerDash;

        //Vector2 vec1 = new Vector2(-5.3f, 0);
        //Vector2 vec2 = new Vector2(-5.560009f, 0);
        //Vector2 res1 = vec1 - vec2;
        //Vector2 res2 = vec2 - vec1;
        //Debug.Log("vec1 - vec2 = " + res1);
        //Debug.Log(res1.normalized);
        //Debug.Log("vec2 - vec1 = " + res2);
        //Debug.Log(res2.normalized);
    }


    private void OnEnable()
    {
        // 启用输入控制
        inputControl.Enable();
        // 监听player的坐标
        EventHandler.OnGetPlayerPosition += GetPlayerPosition;
        // 监听场景加载事件
        loadEvent.LoadRequestEvent += OnLoadSceneEvent;
        afterSceneLoadedEvent.OnEventCalled += OnAfterSceneLoadEvent;
    }

    private void OnDisable()
    {
        inputControl.Disable();
        EventHandler.OnGetPlayerPosition -= GetPlayerPosition;
        loadEvent.LoadRequestEvent += OnLoadSceneEvent;
        afterSceneLoadedEvent.OnEventCalled -= OnAfterSceneLoadEvent;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 读取输入的方向
        inputDirection = inputControl.GamePlay.Movement.ReadValue<Vector2>();

        CheckState();
    }

    private void FixedUpdate()
    {
        // 只有非受击非攻击状态才可以移动
        if(!isHurt && !isAttack)
            Move();
    }

    #endregion

    /// <summary>
    /// 角色移动
    /// </summary>
    public void Move()
    {
        
        rb.velocity = new Vector2(inputDirection.x * currentSpeed * Time.deltaTime, rb.velocity.y);

        #region 人物翻转
        //人物翻转
        float faceDir = transform.localScale.x; 

        if(inputDirection.x == 0)
        {
            //朝向不变
            faceDir = transform.localScale.x;
        } 
        if(inputDirection.x < 0)
        {
            //输入方向  左
            // 角色朝左则不改变方向，角色朝右则需要改变朝向
            faceDir = transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x;
        }
        if (inputDirection.x > 0)
        {
            //输入方向朝右
            // 角色朝右则不改变方向，角色朝左则需要改变朝向
            faceDir = transform.localScale.x > 0 ? transform.localScale.x : - transform.localScale.x;
        }
        #endregion
        transform.localScale = new Vector3(faceDir, transform.localScale.y, transform.localScale.z);
    
    
    }


    private void CheckState()
    {
        // player在地面上使用地面摩擦力，否则无摩擦力
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }


    #region 事件绑定方法

    #region 按键相关
    /// <summary>
    /// 角色跳跃事件  绑定给跳跃按键
    /// </summary>
    /// <param name="obj"></param>
    private void Jump(InputAction.CallbackContext obj)
    {
        // 在地面上时才能跳跃
        // 施加一个向上的瞬时的力
        if (physicsCheck.isGround)
        {
            GetComponent<AudioDefinetion>().PlayAudoClip();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    /// <summary>
    /// 角色攻击事件  绑定给攻击按键
    /// </summary>
    /// <param name="obj"></param>
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        // 未受到伤害时才能触发攻击
        if (!isHurt)
        {
            isAttack = true;
            EventHandler.CallPlayerAttack();
        }
        
    }

    /// <summary>
    /// 切换走路和跑步事件
    /// </summary>
    private void SwitchWalkAndRun(InputAction.CallbackContext obj)
    {
        // 更改跑步状态
        isRun = !isRun;
        // 更改当前速度
        currentSpeed = isRun ? runSpeed : walkSpeed;
    }

    /// <summary>
    /// 角色闪现事件
    /// </summary>
    private void PlayerDash(InputAction.CallbackContext obj)
    {
        // 未受到伤害才能闪现
        if(!isHurt)
        {
            isDash = true;
            lastSpeed = currentSpeed;
            currentSpeed = dashSpeed;
            OnInvulnerableDash?.Invoke();
            //character.InvulnerableDash();
        }
        
    }

    //todo: 如果闪现动画被打断，会出现isDash和速度无法重置的情况
    /// <summary>
    /// 这个方法在闪现动画最后一帧被调用
    /// </summary>
    public void ResetPlayerDash()
    {
        isDash = false;
        currentSpeed = lastSpeed;
    }

    #endregion

    #region UnityEvent
    /// <summary>
    /// 角色受伤
    /// </summary>
    /// <param name="atatcker"></param>
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        // 重置速度
        rb.velocity = Vector2.zero;
        // 受伤被击退
        //Debug.Log(transform.tag + " x = " + transform.position.x);
        //Debug.Log(attacker.tag + " x = " + attacker.position.x);
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), (transform.position.y - attacker.position.y)).normalized;
        //Debug.Log(dir);
        // 添加一个瞬时的力
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// 角色死亡
    /// </summary>
    public void PlayerDead()
    {
        isDead = true;
        // 角色死亡，禁用键盘输入
        inputControl.Disable();
    }

    /// <summary>
    /// 场景加载事件
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadSceneEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        // 禁用游戏时的输入（仅仅是角色控制相关）
        inputControl.GamePlay.Disable();
        
    }

    /// <summary>
    /// 场景完全加载后的事件
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void OnAfterSceneLoadEvent()
    {
        inputControl.GamePlay.Enable();
    }

    #endregion

    #region EventHandler

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    #endregion

    #endregion
}
