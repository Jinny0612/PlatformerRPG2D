using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPatrolState : EnemyBaseStateMachine
{
    private readonly float patrolTouchWallWaitTime = 1f;

    public override void LogicUpdate()
    {
    }

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

    public override void OnExit()
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
}
