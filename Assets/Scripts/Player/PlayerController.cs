using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// ��ɫ���ƽű�
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
    /// ����ķ���
    /// </summary>
    [HideInInspector]public Vector2 inputDirection;

    [Header("�����¼�")]
    /// <summary>
    /// ���������¼�   ���س���ʱ����player���ƶ�
    /// </summary>
    public SceneLoadEventSO loadEvent;
    public VoidEventSO afterSceneLoadedEvent;

    [Header("��������")]
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    [HideInInspector] public float currentSpeed;
    /// <summary>
    /// �ܲ��ٶ�
    /// </summary>
    public float runSpeed;
    /// <summary>
    /// �����ٶ�
    /// </summary>
    public float walkSpeed;
    /// <summary>
    /// �����ٶ�
    /// </summary>
    public float dashSpeed;
    /// <summary>
    /// ֮ǰ���ٶ�  ����ĳЩ��Ϊ������ָ�����ʼ״̬
    /// </summary>
    private float lastSpeed;
    /// <summary>
    /// ��Ծʱ�Խ�ɫʩ��һ������
    /// </summary>
    public float jumpForce;
    /// <summary>
    /// ����ʱ�Խ�ɫʩ��һ���ܻ����������
    /// </summary>
    public float hurtForce;
    

    [Header("�������")]
    /// <summary>
    /// ��ͨ���� ��ʱ����ʹ�ã���Ħ����
    /// </summary>
    public PhysicsMaterial2D normal;
    /// <summary>
    /// ǽ�����  ��ʱ��Ծʹ�ã���Ħ��������ǽ����ֱ�ӻ���
    /// </summary>
    public PhysicsMaterial2D wall;

    [Header("��ɫ״̬")]
    /// <summary>
    /// �Ƿ��ܲ�
    /// </summary>
    public bool isRun;
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    public bool isHurt;
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    public bool isDead;
    /// <summary>
    /// �Ƿ񹥻�
    /// </summary>
    public bool isAttack;
    /// <summary>
    /// �Ƿ�����   �޵�״̬
    /// </summary>
    public bool isDash;

    public UnityEvent OnInvulnerableDash;
    

    #region ��������
    protected override void Awake()
    {
        base.Awake();
        inputControl = new PlayerInputControls();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        coll = GetComponent<CapsuleCollider2D>();
        character = GetComponent<Character>();

        //Ĭ����·
        isRun = false;
        currentSpeed = walkSpeed;

        //��·�ܲ��л�
        inputControl.GamePlay.SwitchBetweenWalkAndRun.started += SwitchWalkAndRun;
        // ������Ծ������˲�䴥���¼�Jump
        inputControl.GamePlay.Jump.started += Jump;
        // �����¼���
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
        // �����������
        inputControl.Enable();
        // ����player������
        EventHandler.OnGetPlayerPosition += GetPlayerPosition;
        // �������������¼�
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
        // ��ȡ����ķ���
        inputDirection = inputControl.GamePlay.Movement.ReadValue<Vector2>();

        CheckState();
    }

    private void FixedUpdate()
    {
        // ֻ�з��ܻ��ǹ���״̬�ſ����ƶ�
        if(!isHurt && !isAttack)
            Move();
    }

    #endregion

    /// <summary>
    /// ��ɫ�ƶ�
    /// </summary>
    public void Move()
    {
        
        rb.velocity = new Vector2(inputDirection.x * currentSpeed * Time.deltaTime, rb.velocity.y);

        #region ���﷭ת
        //���﷭ת
        float faceDir = transform.localScale.x; 

        if(inputDirection.x == 0)
        {
            //���򲻱�
            faceDir = transform.localScale.x;
        } 
        if(inputDirection.x < 0)
        {
            //���뷽��  ��
            // ��ɫ�����򲻸ı䷽�򣬽�ɫ��������Ҫ�ı䳯��
            faceDir = transform.localScale.x < 0 ? transform.localScale.x : -transform.localScale.x;
        }
        if (inputDirection.x > 0)
        {
            //���뷽����
            // ��ɫ�����򲻸ı䷽�򣬽�ɫ��������Ҫ�ı䳯��
            faceDir = transform.localScale.x > 0 ? transform.localScale.x : - transform.localScale.x;
        }
        #endregion
        transform.localScale = new Vector3(faceDir, transform.localScale.y, transform.localScale.z);
    
    
    }


    private void CheckState()
    {
        // player�ڵ�����ʹ�õ���Ħ������������Ħ����
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }


    #region �¼��󶨷���

    #region �������
    /// <summary>
    /// ��ɫ��Ծ�¼�  �󶨸���Ծ����
    /// </summary>
    /// <param name="obj"></param>
    private void Jump(InputAction.CallbackContext obj)
    {
        // �ڵ�����ʱ������Ծ
        // ʩ��һ�����ϵ�˲ʱ����
        if (physicsCheck.isGround)
        {
            GetComponent<AudioDefinetion>().PlayAudoClip();
            rb.velocity = Vector2.zero;
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    /// <summary>
    /// ��ɫ�����¼�  �󶨸���������
    /// </summary>
    /// <param name="obj"></param>
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        // δ�ܵ��˺�ʱ���ܴ�������
        if (!isHurt)
        {
            isAttack = true;
            EventHandler.CallPlayerAttack();
        }
        
    }

    /// <summary>
    /// �л���·���ܲ��¼�
    /// </summary>
    private void SwitchWalkAndRun(InputAction.CallbackContext obj)
    {
        // �����ܲ�״̬
        isRun = !isRun;
        // ���ĵ�ǰ�ٶ�
        currentSpeed = isRun ? runSpeed : walkSpeed;
    }

    /// <summary>
    /// ��ɫ�����¼�
    /// </summary>
    private void PlayerDash(InputAction.CallbackContext obj)
    {
        // δ�ܵ��˺���������
        if(!isHurt)
        {
            isDash = true;
            lastSpeed = currentSpeed;
            currentSpeed = dashSpeed;
            OnInvulnerableDash?.Invoke();
            //character.InvulnerableDash();
        }
        
    }

    //todo: ������ֶ�������ϣ������isDash���ٶ��޷����õ����
    /// <summary>
    /// ������������ֶ������һ֡������
    /// </summary>
    public void ResetPlayerDash()
    {
        isDash = false;
        currentSpeed = lastSpeed;
    }

    #endregion

    #region UnityEvent
    /// <summary>
    /// ��ɫ����
    /// </summary>
    /// <param name="atatcker"></param>
    public void GetHurt(Transform attacker)
    {
        isHurt = true;
        // �����ٶ�
        rb.velocity = Vector2.zero;
        // ���˱�����
        //Debug.Log(transform.tag + " x = " + transform.position.x);
        //Debug.Log(attacker.tag + " x = " + attacker.position.x);
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), (transform.position.y - attacker.position.y)).normalized;
        //Debug.Log(dir);
        // ���һ��˲ʱ����
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// ��ɫ����
    /// </summary>
    public void PlayerDead()
    {
        isDead = true;
        // ��ɫ���������ü�������
        inputControl.Disable();
    }

    /// <summary>
    /// ���������¼�
    /// </summary>
    /// <param name="arg0"></param>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OnLoadSceneEvent(GameSceneSO arg0, Vector3 arg1, bool arg2)
    {
        // ������Ϸʱ�����루�����ǽ�ɫ������أ�
        inputControl.GamePlay.Disable();
        
    }

    /// <summary>
    /// ������ȫ���غ���¼�
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
