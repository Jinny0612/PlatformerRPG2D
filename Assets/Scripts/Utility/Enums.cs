// 枚举类管理

public enum EnemyState
{
    /// <summary>
    /// 巡逻
    /// </summary>
    Patrol,
    /// <summary>
    /// 追击
    /// </summary>
    Chase,
    /// <summary>
    /// 使用技能
    /// </summary>
    Skill
}

/// <summary>
/// 无敌类型
/// </summary>
public enum InvulnerabilityType
{
    /// <summary>
    /// 受伤
    /// </summary>
    Damage,
    /// <summary>
    /// 闪现
    /// </summary>
    Dash
}

/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    /// <summary>
    /// 场景地点
    /// </summary>
    Location,
    /// <summary>
    /// 主菜单
    /// </summary>
    Menu
}

/// <summary>
/// 物品类型
/// </summary>
public enum ItemType
{
    /// <summary>
    /// 装备
    /// </summary>
    Equipment,
    /// <summary>
    /// 药水
    /// </summary>
    Potion,
    /// <summary>
    /// 食物
    /// </summary>
    Food
}
