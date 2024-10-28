/// <summary>
/// 敌人状态机抽象类
/// </summary>
public abstract class EnemyBaseStateMachine
{
    /// <summary>
    /// 当前敌人
    /// </summary>
    protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy enemy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
