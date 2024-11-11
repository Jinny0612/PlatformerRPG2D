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
    /// �ȼ�
    /// </summary>
    public int level;
    /// <summary>
    /// ��ǰ�ȼ������ֵ
    /// </summary>
    public int maxExp;
}

