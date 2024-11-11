using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : SingletonMonoBehvior<EnemyManager>
{
    [SerializeField] private EnemyInfoSO enemyInfoSO;

    [SerializeField] private Dictionary<int, EnemyInfo> enemyInfoDictionary = new Dictionary<int, EnemyInfo>();

    protected override void Awake()
    {
        base.Awake();

        InitEnemyInfoDictionary();
    }

    /// <summary>
    /// 初始化字典
    /// </summary>
    private void InitEnemyInfoDictionary()
    {
        if(enemyInfoSO != null)
        {
            foreach(EnemyInfo info in enemyInfoSO.Enemies)
            {
                enemyInfoDictionary.Add(info.enemyCode, info);
            }
        }
        else
        {
            Debug.Log("enemy信息未配置!");
        }
    }

    /// <summary>
    /// 根据编号获取敌人信息
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public EnemyInfo GetEnemyInfoByCode(int code)
    {
        if(enemyInfoDictionary.Count == 0)
        {
            InitEnemyInfoDictionary ();
            
        }
        return enemyInfoDictionary[code];

    }
}
