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
        //巡逻时是一般的移动速度
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        //更新等待时间
        currentEnemy.waitTime = patrolTouchWallWaitTime;
        currentEnemy.waitTimeCounter = currentEnemy.waitTime;
    }

    public override void OnExit()
    {
        if (currentEnemy.FoundPlayer())
        {
            // 在前方发现player  切换到追击状态
            currentEnemy.SwitchState(EnemyState.Chase);
        }
    }

    public override void PhysicsUpdate()
    {
    }
}
