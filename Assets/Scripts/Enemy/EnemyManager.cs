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
    /// ��ʼ���ֵ�
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
            Debug.Log("enemy��Ϣδ����!");
        }
    }

    /// <summary>
    /// ���ݱ�Ż�ȡ������Ϣ
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
