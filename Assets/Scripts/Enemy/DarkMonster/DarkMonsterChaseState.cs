using UnityEngine;

/// <summary>
/// DarkMonster追击状态
/// </summary>
public class DarkMonsterChaseState : EnemyBaseStateMachine
{
    /// <summary>
    /// 追击时的撞墙等待时间  为了避免原地翻转的问题
    /// </summary>
    private static readonly float chaseTouchWallWaitTime = 0.05f;

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //Debug.Log("enter chase");

        // 追击时使用追击速度，并且切换为跑步
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.isRun = true;

        // 重置计时器
        currentEnemy.waitTime = chaseTouchWallWaitTime;
        currentEnemy.waitTimeCounter = currentEnemy.waitTime;
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
    }
    public override void LogicUpdate()
    {
        if(currentEnemy.lostTimeCounter <= 0)
        {
            //丢失了追击的player超过一定时间，从追击切换到巡逻状态
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
