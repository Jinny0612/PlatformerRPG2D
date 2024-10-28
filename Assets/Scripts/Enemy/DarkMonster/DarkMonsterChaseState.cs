using UnityEngine;

/// <summary>
/// DarkMonster׷��״̬
/// </summary>
public class DarkMonsterChaseState : EnemyBaseStateMachine
{
    /// <summary>
    /// ׷��ʱ��ײǽ�ȴ�ʱ��  Ϊ�˱���ԭ�ط�ת������
    /// </summary>
    private static readonly float chaseTouchWallWaitTime = 0.05f;

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //Debug.Log("enter chase");

        // ׷��ʱʹ��׷���ٶȣ������л�Ϊ�ܲ�
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.isRun = true;

        // ���ü�ʱ��
        currentEnemy.waitTime = chaseTouchWallWaitTime;
        currentEnemy.waitTimeCounter = currentEnemy.waitTime;
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
    }
    public override void LogicUpdate()
    {
        if(currentEnemy.lostTimeCounter <= 0)
        {
            //��ʧ��׷����player����һ��ʱ�䣬��׷���л���Ѳ��״̬
            currentEnemy.SwitchState(EnemyState.Patrol);
        }

        
    }

    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        currentEnemy.isRun = false;
        //Debug.Log("exix chase");
    }
}
