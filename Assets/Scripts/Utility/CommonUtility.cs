using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ͨ�ù�����
/// </summary>
public static class CommonUtility
{
    /// <summary>
    /// ������Ϸʱ�������
    /// </summary>
    public static void EnableGamePlayerContorl()
    {
        PlayerController.Instance.EnablePlayerControl();
    }
    /// <summary>
    /// ������Ϸʱ�������
    /// </summary>
    public static void DisableGamePlayerContorl()
    {
        PlayerController.Instance.DisablePlayerControl();
    }
}
