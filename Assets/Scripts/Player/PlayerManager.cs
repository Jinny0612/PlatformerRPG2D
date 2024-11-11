using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家管理相关 
/// </summary>
public class PlayerManager : SingletonMonoBehvior<PlayerManager>
{
    /// <summary>
    /// 等级经验映射文件
    /// </summary>
    public PlayerExpAndLvlSO PlayerExpAndLvl;

    /// <summary>
    /// 等级经验映射字典key -lvl  value - maxExp
    /// </summary>
    private Dictionary<int,int> playerExpAndLvlDictionary = new Dictionary<int,int>();

    protected override void Awake()
    {
        base.Awake();

        InitializedLvlAndExpDictionary();
    }

    /// <summary>
    /// 初始化等级经验映射字典
    /// </summary>
    private void InitializedLvlAndExpDictionary()
    {
        if(PlayerExpAndLvl == null)
        {
            Debug.Log("玩家等级和经验映射信息未配置!");
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
    /// 获取当前等级最大经验值
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
