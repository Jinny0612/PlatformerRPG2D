// ö�������

public enum EnemyState
{
    /// <summary>
    /// Ѳ��
    /// </summary>
    Patrol,
    /// <summary>
    /// ׷��
    /// </summary>
    Chase,
    /// <summary>
    /// ʹ�ü���
    /// </summary>
    Skill
}

/// <summary>
/// �޵�����
/// </summary>
public enum InvulnerabilityType
{
    /// <summary>
    /// ����
    /// </summary>
    Damage,
    /// <summary>
    /// ����
    /// </summary>
    Dash
}

/// <summary>
/// ��������
/// </summary>
public enum SceneType
{
    /// <summary>
    /// �����ص�
    /// </summary>
    Location,
    /// <summary>
    /// ���˵�
    /// </summary>
    Menu
}

/// <summary>
/// ��Ʒ����
/// </summary>
public enum ItemType
{
    /// <summary>
    /// װ��
    /// </summary>
    Equipment,
    /// <summary>
    /// ҩˮ
    /// </summary>
    Potion,
    /// <summary>
    /// ʳ��
    /// </summary>
    Food,
    /// <summary>
    /// ����
    /// </summary>
    Exp,
    /// <summary>
    /// ���
    /// </summary>
    Coin
}

/// <summary>
/// ��Ʒ��ֵ����
/// </summary>
public enum ItemValuesType
{
    /// <summary>
    /// Ѫ��
    /// </summary>
    Health,
    /// <summary>
    /// ����ֵ
    /// </summary>
    Energy,
    /// <summary>
    /// ������
    /// </summary>
    AttackPoint,
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    MoveSpeed,
    /// <summary>
    /// ����ʱ��,��ʱvalue��λΪ��
    /// </summary>
    Duration
}

/// <summary>
/// ��������
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// ����ֵ
    /// </summary>
    Exp,
    /// <summary>
    /// ���
    /// </summary>
    Coin,
    /// <summary>
    /// ʰȡ��Ʒ
    /// </summary>
    Item
}

/// <summary>
/// ���λ��
/// </summary>
public enum InventoryLocation
{
    /// <summary>
    /// ���
    /// </summary>
    player,
    /// <summary>
    /// ����
    /// </summary>
    chest,
    count
}

/// <summary>
/// �˵�����
/// </summary>
public enum MenuType
{
    /// <summary>
    /// ��
    /// </summary>
    None,
    /// <summary>
    /// ��ɫ��Ϣ
    /// </summary>
    Character,
    /// <summary>
    /// ����
    /// </summary>
    Inventory,
    /// <summary>
    /// ����
    /// </summary>
    Settings
}
