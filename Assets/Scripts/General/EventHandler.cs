using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �¼�����
/// </summary>
public static class EventHandler
{
    #region player���������¼�
    /// <summary>
    /// player�����¼�
    /// </summary>
    public static Action OnPlayerAttack;
    public static void CallPlayerAttack()
    {
        OnPlayerAttack?.Invoke();
    }

    /// <summary>
    /// player�����¼�
    /// </summary>
    public static Action OnPlayerDash;
    public static void CallPlayerDash()
    {
        OnPlayerDash?.Invoke();
    }
    #endregion

    /// <summary>
    /// �����ȡplayer�����ί��
    /// </summary>
    /// <returns></returns>
    public delegate Vector3 GetPlayerPosition();
    /// <summary>
    /// ������ȡplayer�����ί�ж���
    /// </summary>
    public static event GetPlayerPosition OnGetPlayerPosition;

    public static Vector3 CallGetPlayerPosition()
    {
        return (Vector3)(OnGetPlayerPosition?.Invoke());
    }


}
