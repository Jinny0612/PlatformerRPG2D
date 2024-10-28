using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// DarkMonsterѲ��
/// </summary>
public class DarkMonsterPatrolState : EnemyBaseStateMachine
{

    private readonly float patrolTouchWallWaitTime = 1f;

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //Debug.Log("enter patrol");
        //Ѳ��ʱ��һ����ƶ��ٶ�
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        //���µȴ�ʱ��
        currentEnemy.waitTime = patrolTouchWallWaitTime;
        currentEnemy.waitTimeCounter = currentEnemy.waitTime;
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            // ��ǰ������player  �л���׷��״̬
            currentEnemy.SwitchState(EnemyState.Chase);
        } 
    }

    public override void PhysicsUpdate()
    {
        
    }
    public override void OnExit()
    {
        //Debug.Log("exit patrol");
    }
}
