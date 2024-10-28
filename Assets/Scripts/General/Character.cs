using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    /// <summary>
    /// �޵в���
    /// </summary>
    private IInvulnerabilityStrategy invulnerabilityStrategy;
    /// <summary>
    /// �޵м�ʱ������
    /// </summary>
    private InvulnerabilityManager invulnerabilityManager;

    [Header("�¼�����")]
    /// <summary>
    /// ��ʼ����Ϸ�¼�
    /// </summary>
    public VoidEventSO newGameEvent;

    [Header("��������")]
    /// <summary>
    /// ���Ѫ��
    /// </summary>
    public float maxHealth;
    /// <summary>
    /// ��ǰѪ��
    /// </summary>
    public float currentHealth;
    public Rigidbody2D rb;


    [Header("UnityEvent����")]
    /// <summary>
    /// �����¼�
    /// </summary>
    public UnityEvent<Transform> OnTakeDamage;
    /// <summary>
    /// �����¼�
    /// </summary>
    public UnityEvent OnDead;
    /// <summary>
    /// Ѫ������
    /// </summary>
    public UnityEvent<Character> OnHealthChange;



    private void OnEnable()
    {
        newGameEvent.OnEventCalled += NewGameEvent;
    }

    private void OnDisable()
    {
        newGameEvent.OnEventCalled -= NewGameEvent;
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(Tags.WATER))
        {
            //����ˮ�棬����������Ѫ��

            //����Ѫ��
            currentHealth = 0;
            OnHealthChange?.Invoke(this);
            OnDead?.Invoke();//���������¼�
        }
    }

    /// <summary>
    /// ��ʼ����Ϸִ�е��¼�
    /// </summary>
    private void NewGameEvent()
    {
        currentHealth = maxHealth;
        OnHealthChange?.Invoke(this);

        invulnerabilityManager = InvulnerabilityManager.Instance;
        Debug.Log(this.gameObject.name + " = " + currentHealth);

    }

    /// <summary>
    /// �ܵ��˺�  
    /// </summary>
    /// <param name="attacker">������</param>
    public void TakeDamage(Attack attacker)
    {
        //if (invulnerabilityManager != null && invulnerabilityManager.IsInvulnerable(this))
        {
            // �޵�״̬���������߼�
            //Debug.Log("�޵�");
            //return;
        }
        if(currentHealth - attacker.damage > 0)
        {
            // Ѫ�������˺󲻻ήΪ0����ʱ���������޵�״̬
            currentHealth -= attacker.damage;
            // �ܵ��˺�ʱ����ɫ��ʱ�޵�
            invulnerabilityStrategy = InvulnerabilityStrategyFactory.GetStrategy(InvulnerabilityType.Damage,this);
            invulnerabilityStrategy?.TriggerInvulnerable(this,InvulnerabilityType.Damage);
            //Debug.Log(attacker.tag + " currentHealth = " + currentHealth);
            // ִ�������¼�  ���ˡ������˵�
             OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            // �����ٶȣ���������������һ��λ�Ƶ�����
            rb.velocity = Vector3.zero;
            // ִ�������¼�  ������������Ϸ�����ȵ�
            OnDead?.Invoke();
        }
        OnHealthChange?.Invoke(this);
        
    }

    /// <summary>
    /// �����޵�
    /// </summary>
    public void InvulnerableDash()
    {
        // �����޵�
        EventHandler.CallPlayerDash();
        invulnerabilityStrategy = InvulnerabilityStrategyFactory.GetStrategy(InvulnerabilityType.Dash, this);
        invulnerabilityStrategy?.TriggerInvulnerable(this,InvulnerabilityType.Dash);
        
    }

}
