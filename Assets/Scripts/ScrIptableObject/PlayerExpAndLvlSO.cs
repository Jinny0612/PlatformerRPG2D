using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerExpAndLvlSO", menuName = "ScriptableObjects/ObjectsInfo/PlayerExpAndLvlSO")]
public class PlayerExpAndLvlSO : ScriptableObject
{
    [SerializeField] public List<PlayerExpLvl> playerExpLvls;
}

[System.Serializable]
public class PlayerExpLvl
{
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
    /// <summary>
    /// 当前等级最大经验值
    /// </summary>
    public int maxExp;
}

