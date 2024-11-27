using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfoSO",menuName = "ScriptableObjects/ObjectsInfo/EnemyInfoSO")]
public class EnemyInfoSO : ScriptableObject
{
    public List<EnemyInfo> Enemies;
}

[System.Serializable]
public class EnemyInfo
{
    /// <summary>
    /// ���˱�ţ���ͬ���͵ĵ��˱�Ų�ͬ
    /// </summary>
    public int enemyCode;
    /// <summary>
    /// ��������
    /// </summary>
    public string enemyName;
    /// <summary>
    /// ��ɱ����
    /// </summary>
    public int killExp;
    /// <summary>
    /// ������С�����
    /// </summary>
    public int minCoin;
    /// <summary>
    /// �����������
    /// </summary>
    public int maxCoin;
    /// <summary>
    /// ���˵������Ʒ�б�
    /// </summary>
    public List<EnemyDropItem> enemyDropItemList;
    
}

/// <summary>
/// ���������������Ʒs
/// </summary>
[System.Serializable]
public class EnemyDropItem
{
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    [ItemCodeDescription]
    public int itemCode;
    /// <summary>
    /// ��Ʒ���� ���������ʱʹ��
    /// </summary>
    public int itemQuantity;
    /// <summary>
    /// ��Ʒ�����Ƿ��������
    /// </summary>
    public bool isRandom;
    /// <summary>
    /// ��С����
    /// </summary>
    public int minItemQuantity;
    /// <summary>
    /// �������
    /// </summary>
    public int maxItemQuantity;
    /// <summary>
    /// ������� 0-1��Χ
    /// </summary>
    public float dropChance;
}
