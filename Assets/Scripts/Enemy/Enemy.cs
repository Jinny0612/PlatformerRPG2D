using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/// <summary>
/// ���˻���������
/// </summary>
[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;

    [Header("��������")]
    /// <summary>
    /// ���˱��
    /// </summary>
    [EnemyCodeDescription]
    public int enemyCode;

    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    public float normalSpeed;
    /// <summary>
    /// ׷���ٶ�
    /// </summary>
    public float chaseSpeed;
    /// <summary>
    /// ��ǰ�ƶ��ٶ�
    /// </summary>
    public float currentSpeed;
    /// <summary>
    /// �泯����  ��scale���
    /// </summary>
    public Vector3 faceDir;
    /// <summary>
    /// �����˵���
    /// </summary>
    public float hurtForce;
    /// <summary>
    /// ������ս�����ľ���
    /// </summary>
    public float meleeDistance;
    /// <summary>
    /// ����Զ�̹����ľ���
    /// </summary>
    public float remoteDistance;
    

    /// <summary>
    /// ���õ�enemy��Ϣ
    /// </summary>
    private EnemyInfo enemyInfo;

    [Header("Ѫ������")]
    /// <summary>
    /// Ѫ��λ�������enemy���ĵ��ƫ����
    /// </summary>
    public Vector2 healthBarOffset;
    /// <summary>
    /// Ѫ��Ԥ����
    /// </summary>
    public GameObject healthPrefab;
    /// <summary>
    /// ���ɵ�Ѫ��ʵ��
    /// </summary>
    public GameObject curHealthBar;

    /// <summary>
    /// �������˵Ķ���
    /// </summary>
    protected Transform attacker;



    [Header("��ʱ��")]
    /// <summary>
    /// �ȴ�ʱ��
    /// </summary>
    public float waitTime;
    /// <summary>
    /// ��ʱ��
    /// </summary>
    public float waitTimeCounter;
    /// <summary>
    /// �Ƿ��ڵȴ�
    /// </summary>
    public bool wait;
    /// <summary>
    /// ͷ��Ѫ�����ɼ�ʱ��  ���������ò��ܵ������ͱ�Ϊ���ɼ�
    /// </summary>
    public float invisiableUITime;
    /// <summary>
    /// ���ɼ�ʱ���ʱ��
    /// </summary>
    public float invisiableUITimeCounter;


    /// <summary>
    /// ��ʧ׷����playerʱ��
    /// </summary>
    public float lostTime;
    /// <summary>
    /// ��ʱ��
    /// </summary>
    public float lostTimeCounter;
    /// <summary>
    /// ��ս�������
    /// </summary>
    public float meleeCD;
    /// <summary>
    /// ��ս��������ʱ
    /// </summary>
    public float meleeTimeCounter;
    /// <summary>
    /// Զ�̹������
    /// </summary>
    public float remoteCD;
    /// <summary>
    /// Զ�̹�������ʱ
    /// </summary>
    public float remoteTimeCounter;

    [Header("״̬")]
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    public bool isHurt;
    /// <summary>
    /// �Ƿ�����
    /// </summary>
    public bool isDead;
    /// <summary>
    /// �Ƿ���·
    /// </summary>
    public bool isWalk;
    /// <summary>
    /// �Ƿ��ܲ�
    /// </summary>
    public bool isRun;
    /// <summary>
    /// �Ƿ񴥷���ս����
    /// </summary>
    public bool isMelee;
    /// <summary>
    /// �Ƿ񴥷�Զ�̹���
    /// </summary>
    public bool isRemoted;
    /// <summary>
    /// �Ƿ񱻻���
    /// </summary>
    public bool isAwake;

    [Header("���player")]
    /// <summary>
    /// ���ĵ�ƫ����
    /// </summary>
    public Vector2 centerOffset;
    /// <summary>
    /// ��ⷶΧ�ߴ�
    /// </summary>
    public Vector2 checkSize;
    /// <summary>
    /// ������
    /// </summary>
    public float checkDistance;
    /// <summary>
    /// ����ͼ��
    /// </summary>
    public LayerMask attackLayer;

    [Header("enemy����ɱ���")]
    /// <summary>
    /// ��ɱ������Ʒɢ�����
    /// </summary>
    public float scatterForce;
    /// <summary>
    /// ������Ʒ�ĸ�����
    /// </summary>
    private Transform dropItemParent;

    #region unity�¼�

    /// <summary>
    /// ��ս�����¼�
    /// </summary>
    public UnityEvent OnMeleeAttack;
    /// <summary>
    /// Զ�̹����¼�
    /// </summary>
    public UnityEvent OnRemotedAttack;

    #endregion

    #region ״̬��

    /// <summary>
    /// ��ǰ״̬��
    /// </summary>
    private EnemyBaseStateMachine currentState;
    /// <summary>
    /// Ѳ��
    /// </summary>
    protected EnemyBaseStateMachine patrolState;
    /// <summary>
    /// ׷��
    /// </summary>
    protected EnemyBaseStateMachine chaseState;

    #endregion

    #region ��������

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        faceDir = new Vector3(transform.localScale.x, 0, 0);

        // ��ͬ״̬�ȴ�ʱ�䲻ͬ���ȴ�ʱ��ҲΪ�˱�����ײ���ʱԭ�ط�ת����
        waitTimeCounter = waitTime;

        dropItemParent = GameObject.FindGameObjectWithTag(Tags.DROPITEMPARENT)?.transform;

    }

    private void Start()
    {
        enemyInfo = EnemyManager.Instance.GetEnemyInfoByCode(enemyCode);
    }

    private void OnEnable()
    {
        // Ĭ��ΪѲ��״̬
        currentState = patrolState;
        currentState.OnEnter(this);

        //EventHandler.OnEnemyHealthUIBarChangeEvent += InstantiateHealthBar;
    }

    private void OnDisable()
    {
        currentState.OnExit();
        //EventHandler.OnEnemyHealthUIBarChangeEvent -= InstantiateHealthBar;
    }

    private void Update()
    {
        // player��λ�ã�����player
        //Vector3 playerPosition = EventHandler.CallGetPlayerPosition();
        // �����泯player����
        //faceDir = new Vector3((playerPosition - transform.position).x, 0 , 0).normalized;
        faceDir = new Vector3(transform.localScale.x, 0, 0);

        if (!physicsCheck.isGround || physicsCheck.touchWall)
        {
            // ���������Ҫ��enemyǰ������������
            //�������δ��⵽���棬ԭ�صȴ�����ͷ || ײǽ��ԭ�صȴ�һ��ʱ����ͷ
            wait = true;
        }
        else
        {
            // δײǽ������ȴ�
            wait = false;
        }

        #region ��������

        //��ս
        PerformMeleeAttack();
        //Զ��
        PerformRemotedAttack();

        #endregion

        #region Ѫ���仯  ����enemy�ƶ���ֻ��enemy�����ƶ�ʱ�Ÿ���

        // ����characterʱֻ����λ��
        if (curHealthBar != null)
        {
            EventHandler.CallEnemyHealthUIBarChangeEvent(null, this, curHealthBar);
        }

        #endregion

        TimeCounter();

        currentState.LogicUpdate();
        /*else
        {
            transform.localScale = new Vector3(faceDir.x, 1, 1);
        }*/

    }

    private void FixedUpdate()
    {
        if (!isHurt && !isMelee && !isRemoted && !isDead && !wait)
        {
            Move();

            
        }
        currentState.PhysicsUpdate();
    }

    #endregion

    /// <summary>
    /// �ƶ�����
    /// </summary>
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    /// <summary>
    /// ��ʱ��
    /// </summary>
    public void TimeCounter()
    {
        if(wait)
        {
            waitTimeCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            if(waitTimeCounter <= 0)
            {
                ResetWaitParam();
                // ��ͷ
                transform.localScale = new Vector3(-faceDir.x, transform.localScale.y, transform.localScale.z);
                
            }
        }

        // ��ʧ��׷����player ���Ҽ�ʱ������ʱδ����
        if (!FoundPlayer() && lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;

        }
        else if(FoundPlayer()) 
        {
            //������player���ü�ʱ��
            lostTimeCounter = lostTime;
        }

        #region ����CD

        if(meleeTimeCounter > 0)
        {
            meleeTimeCounter -= Time.deltaTime;
        }

        if(remoteTimeCounter > 0)
        {
            remoteTimeCounter -= Time.deltaTime;
        }

        #endregion

        #region ͷ��Ѫ����ʱ��
        if(curHealthBar != null)
        {
            if (invisiableUITimeCounter > 0)
            {
                // ����ʱ
                invisiableUITimeCounter -= Time.deltaTime;
            }
            else
            {
                curHealthBar.SetActive(false);
            }
        }
        

        #endregion
    }

    /// <summary>
    /// ͷ��Ѫ����ʱ������
    /// </summary>
    public void ResetHealtharBarInvisiableTime()
    {
        invisiableUITimeCounter = invisiableUITime;
    }



    /// <summary>
    /// ���ü�ʱ����ز���  ���˿����ƶ�
    /// </summary>
    protected void ResetWaitParam()
    {
        wait = false;
        waitTimeCounter = waitTime;
    }

    /// <summary>
    /// ���player
    /// </summary>
    /// <returns></returns>
    public bool FoundPlayer()
    {
        // enemyָ��player�ķ���
        Vector3 directionToPlayer = EventHandler.CallGetPlayerPosition() - transform.position;
        if (Vector3.Dot(faceDir, directionToPlayer.normalized) > 0)
        {
            // enemy�泯����  ��  enemyָ��player����  ��������
            // >0��ʾ�н�С��90�㼴��������   <0 ��ʾ�нǴ���90�㼴�����෴   =0��ʾ��ֱ
            // ֻ�ڷ�������ʱ��⣬�������enemy��������
            return Physics2D.BoxCast(transform.position + new Vector3(faceDir.x * centerOffset.x, centerOffset.y, 0), checkSize, 0, faceDir, checkDistance, attackLayer);
        }
        return false;
    }

    /// <summary>
    /// ������ս����
    /// </summary>
    private void PerformMeleeAttack()
    {
        if(FoundPlayer() && meleeTimeCounter <= 0 
            && Vector2.Distance(EventHandler.CallGetPlayerPosition(),transform.position) <= meleeDistance)
        {
            // ��⵽player������cd�У��ڹ���������  ������������
            isMelee = true;
            rb.velocity = Vector3.zero;
            meleeTimeCounter = meleeCD;
            // ������ս�����¼�
            OnMeleeAttack?.Invoke();
        }
    }

    /// <summary>
    /// ����Զ�̹���
    /// </summary>
    private void PerformRemotedAttack()
    {
        if (FoundPlayer() && remoteTimeCounter <= 0
            && Vector2.Distance(EventHandler.CallGetPlayerPosition(), transform.position) <= remoteDistance && Vector2.Distance(EventHandler.CallGetPlayerPosition(), transform.position) > meleeDistance)
        {
            // ��⵽player������cd�У��ڹ���������(������ս�������벢����Զ�̹���������)  ������������
            isRemoted = true;
            rb.velocity = Vector3.zero;
            remoteTimeCounter = remoteCD;
            // ������ս�����¼�
            OnRemotedAttack?.Invoke();
        }
    }

    /// <summary>
    /// ״̬�л�
    /// </summary>
    /// <param name="state"></param>
    public void SwitchState(EnemyState state)
    {
        EnemyBaseStateMachine newState = currentState;
        switch (state)
        {
            case EnemyState.Patrol:
                newState = patrolState; break;
            case EnemyState.Chase:
                newState = chaseState; break;
            default: break;
        }
        // ���˳���ǰ״̬�����л������뵽��״̬
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    #region Event

    /// <summary>
    /// �����ܵ������߼�
    /// </summary>
    /// <param name="attackTrans"></param>
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;

        // ���µ���Ѫ��
        EventHandler.CallEnemyHealthUIBarChangeEvent(GetComponent<Character>(), this, curHealthBar);

        // player�ڵ��˱��󹥻���������Ҫ������ͷ
        if ((transform.localScale.x < 0 && attacker.position.x - transform.position.x > 0)
            || (transform.localScale.x > 0 && attacker.position.x - transform.position.x < 0))
        {
            // transform.localScale.x < 0���˳���player�ڵ����ұ�
            // transform.localScale.x > 0���˳��ң�player�ڵ������
            // ������Ҫ��ͷ�����Ҳ�����wait״̬
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            ResetWaitParam();
        }

        //���˱�����
        isHurt = true;
        rb.velocity = new Vector3(0,rb.velocity.y);
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        // �ȴ����˶���������isHurt����,������ȴ��������������ã����̫�̼���û��ͣ��   �������0.5s
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    /// <summary>
    /// �����߼�
    /// </summary>
    public void EnemyDead()
    {
        //����ͣ��
        rb.velocity = Vector3.zero;
        //������layer��Ϊ2-Ignore Raycast
        //������project settings->Physics2D���޸�Player��Ignore Raycast��������ײ
        //��������������player�������������ײ
        //TODO:�о�Ҳ����������������ʼ�ͽ���������ĵ�����ײ���Ϊ�����ã������Ա�һ��
        gameObject.layer = 2;
        isDead = true;

        // ����Ѫ��
        EventHandler.CallDestroyHealthUIBarEvent(this);

        #region �������úõ���Ʒ

        // ���ɾ������Ӻͽ�����ӵ�����
        DropExpAndCoinAfterDead();

        // ���ɵ�����Ʒ
        InstantiageDropItems();
        #endregion

    }

    /// <summary>
    /// ���ɵ�����Ʒ
    /// </summary>
    private void InstantiageDropItems()
    {
        //todo: ��Ʒ�����߼������⣬ͬʱ��ɱ������ˣ����ظ�����������
        List<EnemyDropItem> dropItems = enemyInfo.enemyDropItemList;

        foreach(EnemyDropItem item in dropItems)
        {
            //����0-1֮��������
            float randomValue = Random.Range(0f, 1f);
            //�����С���趨�ĵ�����ʣ�������Ʒ
            if(randomValue <= item.dropChance)
            {
                // ��ȡԤ����
                GameObject prefab = InventoryManager.Instance.GetItemDetailByCode(item.itemCode)?.prefab;
                // ����Ԥ����ʵ��
                GameObject droppedItem = Instantiate(prefab,transform.position,Quaternion.identity,dropItemParent);

                // ��ȡrigidbody���
                Rigidbody2D rb = droppedItem.GetComponent<Rigidbody2D>();
                Debug.Log("������Ʒ - " + item.itemCode);
                if(rb != null)
                {
                    rb.drag = 2f;//�������������������Ʒ�������ٶ�
                    rb.angularDrag = 2f;//��ӽ�������������Ʒ����ת�ٶ�
                    // ����Ʒ����һ�����������ģ��ɢ��Ч��
                    Vector2 randomDir = Random.insideUnitCircle.normalized;//�������
                    Vector2 force = randomDir * scatterForce;
                    // ���˲ʱ������
                    rb.AddForce(force, ForceMode2D.Impulse);

                    // �����ת��������Ȼ��ɢ��Ч��
                    float randomRotation = Random.Range(0f, 360f);
                    rb.rotation = randomRotation;
                }
            }
        }

    }

    /// <summary>
    /// �������������������������
    /// ��������������������һ֡������
    /// </summary>
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// ��ս�����������һ֡������
    /// </summary>
    public void OnMeleeAttackEnd()
    {
        isMelee = false;

    }
    /// <summary>
    /// Զ�̹����������һ֡������
    /// </summary>
    public void OnRemotedAttackEne()
    {
        isRemoted = false;
    }



    #endregion

    #region ˽�з���

    /// <summary>
    /// �������侭�ͽ��
    /// </summary>
    private void DropExpAndCoinAfterDead()
    {
        // ����
        DropExp();
        //todo: ��������

        // ���
        DropCoin();
        //todo: �������

    }

    /// <summary>
    /// ����player��������
    /// </summary>
    private void DropExp()
    {
        EventHandler.CallSetPlayerExpEvent(enemyInfo.killExp);
    }

    /// <summary>
    /// ����player�������
    /// </summary>
    private void DropCoin()
    {
        int quantity = enemyInfo.minCoin;
        if(enemyInfo.maxCoin > 0)
        {
            quantity = Random.Range(enemyInfo.minCoin, enemyInfo.maxCoin + 1);
        }
        EventHandler.CallSetPlayerCoinEvent(quantity);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance * transform.localScale.x,0,0), 0.2f);
        Vector3 startPoint = transform.position + new Vector3(faceDir.x * centerOffset.x, centerOffset.y, 0);
        Vector3 endPoint = startPoint + (Vector3)faceDir * checkDistance;
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(startPoint, checkSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(endPoint, checkSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startPoint, endPoint);

        Gizmos.DrawWireSphere(transform.position + (Vector3)healthBarOffset, 0.2f);
    }
}
