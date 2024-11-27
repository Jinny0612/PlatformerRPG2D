using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/// <summary>
/// 敌人基础控制类
/// </summary>
[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;

    [Header("基本参数")]
    /// <summary>
    /// 敌人编号
    /// </summary>
    [EnemyCodeDescription]
    public int enemyCode;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float normalSpeed;
    /// <summary>
    /// 追击速度
    /// </summary>
    public float chaseSpeed;
    /// <summary>
    /// 当前移动速度
    /// </summary>
    public float currentSpeed;
    /// <summary>
    /// 面朝方向  与scale相关
    /// </summary>
    public Vector3 faceDir;
    /// <summary>
    /// 被击退的力
    /// </summary>
    public float hurtForce;
    /// <summary>
    /// 触发近战攻击的距离
    /// </summary>
    public float meleeDistance;
    /// <summary>
    /// 触发远程攻击的距离
    /// </summary>
    public float remoteDistance;
    

    /// <summary>
    /// 配置的enemy信息
    /// </summary>
    private EnemyInfo enemyInfo;

    [Header("血条管理")]
    /// <summary>
    /// 血条位置相对于enemy中心点的偏移量
    /// </summary>
    public Vector2 healthBarOffset;
    /// <summary>
    /// 血条预制体
    /// </summary>
    public GameObject healthPrefab;
    /// <summary>
    /// 生成的血条实例
    /// </summary>
    public GameObject curHealthBar;

    /// <summary>
    /// 攻击敌人的对象
    /// </summary>
    protected Transform attacker;



    [Header("计时器")]
    /// <summary>
    /// 等待时间
    /// </summary>
    public float waitTime;
    /// <summary>
    /// 计时器
    /// </summary>
    public float waitTimeCounter;
    /// <summary>
    /// 是否在等待
    /// </summary>
    public bool wait;
    /// <summary>
    /// 头部血条不可见时间  例如持续多久不受到攻击就变为不可见
    /// </summary>
    public float invisiableUITime;
    /// <summary>
    /// 不可见时间计时器
    /// </summary>
    public float invisiableUITimeCounter;


    /// <summary>
    /// 丢失追击的player时间
    /// </summary>
    public float lostTime;
    /// <summary>
    /// 计时器
    /// </summary>
    public float lostTimeCounter;
    /// <summary>
    /// 近战攻击间隔
    /// </summary>
    public float meleeCD;
    /// <summary>
    /// 近战攻击倒计时
    /// </summary>
    public float meleeTimeCounter;
    /// <summary>
    /// 远程攻击间隔
    /// </summary>
    public float remoteCD;
    /// <summary>
    /// 远程攻击倒计时
    /// </summary>
    public float remoteTimeCounter;

    [Header("状态")]
    /// <summary>
    /// 是否受伤
    /// </summary>
    public bool isHurt;
    /// <summary>
    /// 是否死亡
    /// </summary>
    public bool isDead;
    /// <summary>
    /// 是否走路
    /// </summary>
    public bool isWalk;
    /// <summary>
    /// 是否跑步
    /// </summary>
    public bool isRun;
    /// <summary>
    /// 是否触发近战攻击
    /// </summary>
    public bool isMelee;
    /// <summary>
    /// 是否触发远程攻击
    /// </summary>
    public bool isRemoted;
    /// <summary>
    /// 是否被唤醒
    /// </summary>
    public bool isAwake;

    [Header("检测player")]
    /// <summary>
    /// 中心点偏移量
    /// </summary>
    public Vector2 centerOffset;
    /// <summary>
    /// 检测范围尺寸
    /// </summary>
    public Vector2 checkSize;
    /// <summary>
    /// 检测距离
    /// </summary>
    public float checkDistance;
    /// <summary>
    /// 检测的图层
    /// </summary>
    public LayerMask attackLayer;

    [Header("enemy被击杀相关")]
    /// <summary>
    /// 击杀掉落物品散落的力
    /// </summary>
    public float scatterForce;
    /// <summary>
    /// 掉落物品的父物体
    /// </summary>
    private Transform dropItemParent;

    #region unity事件

    /// <summary>
    /// 近战攻击事件
    /// </summary>
    public UnityEvent OnMeleeAttack;
    /// <summary>
    /// 远程攻击事件
    /// </summary>
    public UnityEvent OnRemotedAttack;

    #endregion

    #region 状态机

    /// <summary>
    /// 当前状态机
    /// </summary>
    private EnemyBaseStateMachine currentState;
    /// <summary>
    /// 巡逻
    /// </summary>
    protected EnemyBaseStateMachine patrolState;
    /// <summary>
    /// 追击
    /// </summary>
    protected EnemyBaseStateMachine chaseState;

    #endregion

    #region 生命周期

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        faceDir = new Vector3(transform.localScale.x, 0, 0);

        // 不同状态等待时间不同，等待时间也为了避免碰撞检测时原地翻转问题
        waitTimeCounter = waitTime;

        dropItemParent = GameObject.FindGameObjectWithTag(Tags.DROPITEMPARENT)?.transform;

    }

    private void Start()
    {
        enemyInfo = EnemyManager.Instance.GetEnemyInfoByCode(enemyCode);
    }

    private void OnEnable()
    {
        // 默认为巡逻状态
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
        // player的位置，面向player
        //Vector3 playerPosition = EventHandler.CallGetPlayerPosition();
        // 敌人面朝player方向
        //faceDir = new Vector3((playerPosition - transform.position).x, 0 , 0).normalized;
        faceDir = new Vector3(transform.localScale.x, 0, 0);

        if (!physicsCheck.isGround || physicsCheck.touchWall)
        {
            // 地面检测点需要在enemy前方，否则会掉落
            //地面检测点未检测到地面，原地等待并掉头 || 撞墙则原地等待一段时间后掉头
            wait = true;
        }
        else
        {
            // 未撞墙，无需等待
            wait = false;
        }

        #region 攻击技能

        //近战
        PerformMeleeAttack();
        //远程
        PerformRemotedAttack();

        #endregion

        #region 血条变化  随着enemy移动，只有enemy可以移动时才更新

        // 不传character时只更新位置
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
    /// 移动方法
    /// </summary>
    public virtual void Move()
    {
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    /// <summary>
    /// 计时器
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
                // 掉头
                transform.localScale = new Vector3(-faceDir.x, transform.localScale.y, transform.localScale.z);
                
            }
        }

        // 丢失了追击的player 并且计时器倒计时未结束
        if (!FoundPlayer() && lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;

        }
        else if(FoundPlayer()) 
        {
            //发现了player重置计时器
            lostTimeCounter = lostTime;
        }

        #region 技能CD

        if(meleeTimeCounter > 0)
        {
            meleeTimeCounter -= Time.deltaTime;
        }

        if(remoteTimeCounter > 0)
        {
            remoteTimeCounter -= Time.deltaTime;
        }

        #endregion

        #region 头部血条计时器
        if(curHealthBar != null)
        {
            if (invisiableUITimeCounter > 0)
            {
                // 倒计时
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
    /// 头部血条计时器重置
    /// </summary>
    public void ResetHealtharBarInvisiableTime()
    {
        invisiableUITimeCounter = invisiableUITime;
    }



    /// <summary>
    /// 重置计时器相关参数  敌人可以移动
    /// </summary>
    protected void ResetWaitParam()
    {
        wait = false;
        waitTimeCounter = waitTime;
    }

    /// <summary>
    /// 检测player
    /// </summary>
    /// <returns></returns>
    public bool FoundPlayer()
    {
        // enemy指向player的方向
        Vector3 directionToPlayer = EventHandler.CallGetPlayerPosition() - transform.position;
        if (Vector3.Dot(faceDir, directionToPlayer.normalized) > 0)
        {
            // enemy面朝方向  与  enemy指向player方向  的向量积
            // >0表示夹角小于90°即方向相似   <0 表示夹角大于90°即方向相反   =0表示垂直
            // 只在方向相似时检测，即不检测enemy背后区域
            return Physics2D.BoxCast(transform.position + new Vector3(faceDir.x * centerOffset.x, centerOffset.y, 0), checkSize, 0, faceDir, checkDistance, attackLayer);
        }
        return false;
    }

    /// <summary>
    /// 触发近战攻击
    /// </summary>
    private void PerformMeleeAttack()
    {
        if(FoundPlayer() && meleeTimeCounter <= 0 
            && Vector2.Distance(EventHandler.CallGetPlayerPosition(),transform.position) <= meleeDistance)
        {
            // 检测到player，不在cd中，在攻击距离内  触发攻击动画
            isMelee = true;
            rb.velocity = Vector3.zero;
            meleeTimeCounter = meleeCD;
            // 触发近战攻击事件
            OnMeleeAttack?.Invoke();
        }
    }

    /// <summary>
    /// 触发远程攻击
    /// </summary>
    private void PerformRemotedAttack()
    {
        if (FoundPlayer() && remoteTimeCounter <= 0
            && Vector2.Distance(EventHandler.CallGetPlayerPosition(), transform.position) <= remoteDistance && Vector2.Distance(EventHandler.CallGetPlayerPosition(), transform.position) > meleeDistance)
        {
            // 检测到player，不在cd中，在攻击距离内(超出近战攻击距离并且在远程攻击距离内)  触发攻击动画
            isRemoted = true;
            rb.velocity = Vector3.zero;
            remoteTimeCounter = remoteCD;
            // 触发近战攻击事件
            OnRemotedAttack?.Invoke();
        }
    }

    /// <summary>
    /// 状态切换
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
        // 先退出当前状态，再切换并进入到新状态
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    #region Event

    /// <summary>
    /// 敌人受到攻击逻辑
    /// </summary>
    /// <param name="attackTrans"></param>
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;

        // 更新敌人血条
        EventHandler.CallEnemyHealthUIBarChangeEvent(GetComponent<Character>(), this, curHealthBar);

        // player在敌人背后攻击，敌人需要立即掉头
        if ((transform.localScale.x < 0 && attacker.position.x - transform.position.x > 0)
            || (transform.localScale.x > 0 && attacker.position.x - transform.position.x < 0))
        {
            // transform.localScale.x < 0敌人朝左，player在敌人右边
            // transform.localScale.x > 0敌人朝右，player在敌人左边
            // 敌人需要掉头，并且不处于wait状态
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            ResetWaitParam();
        }

        //受伤被击退
        isHurt = true;
        rb.velocity = new Vector3(0,rb.velocity.y);
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        StartCoroutine(OnHurt(dir));
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        // 等待受伤动画结束将isHurt重置,如果不等待动画结束便重置，间隔太短几乎没有停顿   动画大概0.5s
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    /// <summary>
    /// 死亡逻辑
    /// </summary>
    public void EnemyDead()
    {
        //立即停下
        rb.velocity = Vector3.zero;
        //将敌人layer改为2-Ignore Raycast
        //并且在project settings->Physics2D中修改Player与Ignore Raycast不产生碰撞
        //这样敌人死亡后player经过不会产生碰撞
        //TODO:感觉也可以在死亡动画开始就将这个死亡的敌人碰撞体改为不启用，后续对比一下
        gameObject.layer = 2;
        isDead = true;

        // 销毁血条
        EventHandler.CallDestroyHealthUIBarEvent(this);

        #region 掉落配置好的物品

        // 生成经验增加和金币增加的提醒
        DropExpAndCoinAfterDead();

        // 生成掉落物品
        InstantiageDropItems();
        #endregion

    }

    /// <summary>
    /// 生成掉落物品
    /// </summary>
    private void InstantiageDropItems()
    {
        //todo: 物品掉落逻辑有问题，同时击杀多个敌人，会重复掉落多个物体
        List<EnemyDropItem> dropItems = enemyInfo.enemyDropItemList;

        foreach(EnemyDropItem item in dropItems)
        {
            //生成0-1之间的随机数
            float randomValue = Random.Range(0f, 1f);
            //随机数小于设定的掉落概率，掉落物品
            if(randomValue <= item.dropChance)
            {
                // 获取预制体
                GameObject prefab = InventoryManager.Instance.GetItemDetailByCode(item.itemCode)?.prefab;
                // 生成预制体实例
                GameObject droppedItem = Instantiate(prefab,transform.position,Quaternion.identity,dropItemParent);

                // 获取rigidbody组件
                Rigidbody2D rb = droppedItem.GetComponent<Rigidbody2D>();
                Debug.Log("掉落物品 - " + item.itemCode);
                if(rb != null)
                {
                    rb.drag = 2f;//添加线性阻力，减少物品的下落速度
                    rb.angularDrag = 2f;//添加角阻力，减少物品的旋转速度
                    // 给物品增加一个随机的力，模拟散落效果
                    Vector2 randomDir = Random.insideUnitCircle.normalized;//随机方向
                    Vector2 force = randomDir * scatterForce;
                    // 添加瞬时的推力
                    rb.AddForce(force, ForceMode2D.Impulse);

                    // 随机旋转，增加自然的散落效果
                    float randomRotation = Random.Range(0f, 360f);
                    rb.rotation = randomRotation;
                }
            }
        }

    }

    /// <summary>
    /// 死亡动画结束后销毁这个物体
    /// 这个方法在死亡动画最后一帧被调用
    /// </summary>
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// 近战攻击结束最后一帧被调用
    /// </summary>
    public void OnMeleeAttackEnd()
    {
        isMelee = false;

    }
    /// <summary>
    /// 远程攻击结束最后一帧被调用
    /// </summary>
    public void OnRemotedAttackEne()
    {
        isRemoted = false;
    }



    #endregion

    #region 私有方法

    /// <summary>
    /// 死亡掉落经和金币
    /// </summary>
    private void DropExpAndCoinAfterDead()
    {
        // 经验
        DropExp();
        //todo: 经验提醒

        // 金币
        DropCoin();
        //todo: 金币提醒

    }

    /// <summary>
    /// 死亡player经验增加
    /// </summary>
    private void DropExp()
    {
        EventHandler.CallSetPlayerExpEvent(enemyInfo.killExp);
    }

    /// <summary>
    /// 死亡player金币增加
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
