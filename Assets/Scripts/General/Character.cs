using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    /// <summary>
    /// 无敌策略
    /// </summary>
    private IInvulnerabilityStrategy invulnerabilityStrategy;
    /// <summary>
    /// 无敌计时器管理
    /// </summary>
    private InvulnerabilityManager invulnerabilityManager;

    [Header("事件监听")]
    /// <summary>
    /// 开始新游戏事件
    /// </summary>
    public VoidEventSO newGameEvent;

    [Header("基础属性")]
    /// <summary>
    /// 最大血量
    /// </summary>
    public float maxHealth;
    /// <summary>
    /// 当前血量
    /// </summary>
    public float currentHealth;
    public Rigidbody2D rb;


    [Header("UnityEvent管理")]
    /// <summary>
    /// 受伤事件
    /// </summary>
    public UnityEvent<Transform> OnTakeDamage;
    /// <summary>
    /// 死亡事件
    /// </summary>
    public UnityEvent OnDead;
    /// <summary>
    /// 血量更新
    /// </summary>
    public UnityEvent<Character> OnHealthChange;


    private void OnEnable()
    {
        //newGameEvent.OnEventCalled += NewGameEvent;

        EventHandler.OnResetCharacterBasicInfoWhenLoadSceneEvent += NewGameEvent;
    }

    private void Start()
    {
        invulnerabilityManager = InvulnerabilityManager.Instance;
    }

    private void OnDisable()
    {
        //newGameEvent.OnEventCalled -= NewGameEvent;

        EventHandler.OnResetCharacterBasicInfoWhenLoadSceneEvent -= NewGameEvent;
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(Tags.WATER))
        {
            //掉入水面，死亡、更新血量

            //更新血量
            currentHealth = 0;
            //OnHealthChange?.Invoke(this);

            if(this.CompareTag(Tags.PLAYER))
            {
                EventHandler.CallHealthChangeEvent(this);
            }
            else if(this.CompareTag(Tags.ENEMY))
            {
                Enemy enemy = GetComponent<Enemy>();
                EventHandler.CallEnemyHealthUIBarChangeEvent(this, enemy,enemy.curHealthBar);
                EventHandler.CallDestroyHealthUIBarEvent(enemy);
            }
            
            OnDead?.Invoke();//触发死亡事件
        }
    }

    /// <summary>
    /// 开始新游戏执行的事件
    /// </summary>
    private void NewGameEvent()
    {
        currentHealth = maxHealth;
        //OnHealthChange?.Invoke(this);
        //EventHandler.CallHealthChangeEvent(this);

        if (this.CompareTag(Tags.PLAYER))
        {
            EventHandler.CallHealthChangeEvent(this);
        }
        

    }


    /// <summary>
    /// 受到伤害  
    /// </summary>
    /// <param name="attacker">攻击者</param>
    public void TakeDamage(Attack attacker)
    {
        //if (invulnerabilityManager != null && invulnerabilityManager.IsInvulnerable(this))
        {
            // 无敌状态，无受伤逻辑
            //Debug.Log("无敌");
            //return;
        }
        Enemy enemy = GetComponent<Enemy>();

        if (currentHealth - attacker.damage > 0)
        {
            // 血量在受伤后不会降为0，此时触发受伤无敌状态
            currentHealth -= attacker.damage;
            // 受到伤害时，角色暂时无敌
            invulnerabilityStrategy = InvulnerabilityStrategyFactory.GetStrategy(InvulnerabilityType.Damage,this);
            invulnerabilityStrategy?.TriggerInvulnerable(this,InvulnerabilityType.Damage);
            //Debug.Log(attacker.tag + " currentHealth = " + currentHealth);
            // 执行受伤事件  受伤、被击退等
             OnTakeDamage?.Invoke(attacker.transform);

            if (this.CompareTag(Tags.ENEMY))
            {
                // 更新敌人血条
                EventHandler.CallEnemyHealthUIBarChangeEvent(this, enemy, enemy.curHealthBar);
            }
        }
        else
        {
            currentHealth = 0;
            // 重置速度，避免死亡还出现一定位移的问题
            rb.velocity = Vector3.zero;
            // 执行死亡事件  死亡动画、游戏结束等等
            OnDead?.Invoke();
            if(this.CompareTag(Tags.ENEMY) )
            {
                // 销毁血条
                EventHandler.CallDestroyHealthUIBarEvent(enemy);
            }
        }
        OnHealthChange?.Invoke(this);

        
    }

    /// <summary>
    /// 闪现无敌
    /// </summary>
    public void InvulnerableDash()
    {
        // 闪现无敌
        EventHandler.CallPlayerDash();
        invulnerabilityStrategy = InvulnerabilityStrategyFactory.GetStrategy(InvulnerabilityType.Dash, this);
        invulnerabilityStrategy?.TriggerInvulnerable(this,InvulnerabilityType.Dash);
        
    }

}
