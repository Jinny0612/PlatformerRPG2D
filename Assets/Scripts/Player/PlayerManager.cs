using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҹ������ 
/// </summary>
public class PlayerManager : SingletonMonoBehvior<PlayerManager>
{
    /// <summary>
    /// �ȼ�����ӳ���ļ�
    /// </summary>
    public PlayerExpAndLvlSO PlayerExpAndLvl;

    /// <summary>
    /// �ȼ�����ӳ���ֵ�key -lvl  value - maxExp
    /// </summary>
    private Dictionary<int,int> playerExpAndLvlDictionary = new Dictionary<int,int>();

    protected override void Awake()
    {
        base.Awake();

        InitializedLvlAndExpDictionary();
    }

    /// <summary>
    /// ��ʼ���ȼ�����ӳ���ֵ�
    /// </summary>
    private void InitializedLvlAndExpDictionary()
    {
        if(PlayerExpAndLvl == null)
        {
            Debug.Log("��ҵȼ��;���ӳ����Ϣδ����!");
            return;
        }

        if(playerExpAndLvlDictionary.Count == 0)
        {
            foreach(PlayerExpLvl info in PlayerExpAndLvl.playerExpLvls) 
            {
                playerExpAndLvlDictionary.Add(info.level, info.maxExp);
            }
        }
    }

    /// <summary>
    /// ��ȡ��ǰ�ȼ������ֵ
    /// </summary>
    /// <param name="curentLevel"></param>
    /// <returns></returns>
    public int GetMaxExpInCurLevel(int curentLevel)
    {
        if(playerExpAndLvlDictionary.Count == 0)
        {
            InitializedLvlAndExpDictionary();
        }
        return playerExpAndLvlDictionary[curentLevel];
    }
}
