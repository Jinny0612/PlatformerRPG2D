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
    Food,
    /// <summary>
    /// 经验
    /// </summary>
    Exp,
    /// <summary>
    /// 金币
    /// </summary>
    Coin
}

/// <summary>
/// 物品数值类型
/// </summary>
public enum ItemValuesType
{
    /// <summary>
    /// 血量
    /// </summary>
    Health,
    /// <summary>
    /// 精力值
    /// </summary>
    Energy,
    /// <summary>
    /// 攻击力
    /// </summary>
    AttackPoint,
    /// <summary>
    /// 移动速度
    /// </summary>
    MoveSpeed,
    /// <summary>
    /// 持续时间,此时value单位为秒
    /// </summary>
    Duration
}

/// <summary>
/// 提醒类型
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// 经验值
    /// </summary>
    Exp,
    /// <summary>
    /// 金币
    /// </summary>
    Coin,
    /// <summary>
    /// 拾取物品
    /// </summary>
    Item
}

/// <summary>
/// 库存位置
/// </summary>
public enum InventoryLocation
{
    /// <summary>
    /// 玩家
    /// </summary>
    player,
    /// <summary>
    /// 箱子
    /// </summary>
    chest,
    count
}

/// <summary>
/// 菜单类型
/// </summary>
public enum MenuType
{
    /// <summary>
    /// 无
    /// </summary>
    None,
    /// <summary>
    /// 角色信息
    /// </summary>
    Character,
    /// <summary>
    /// 背包
    /// </summary>
    Inventory,
    /// <summary>
    /// 设置
    /// </summary>
    Settings
}
