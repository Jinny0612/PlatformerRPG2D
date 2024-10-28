/// <summary>
/// ����״̬��������
/// </summary>
public abstract class EnemyBaseStateMachine
{
    /// <summary>
    /// ��ǰ����
    /// </summary>
    protected Enemy currentEnemy;
    public abstract void OnEnter(Enemy enemy);
    public abstract void LogicUpdate();
    public abstract void PhysicsUpdate();
    public abstract void OnExit();
}
