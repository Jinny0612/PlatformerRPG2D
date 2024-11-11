using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NotificationPrefabSO", menuName = "ScriptableObjects/ObjectsInfo/NotificationPrefabSO")]
public class NotificationPrefabSO : ScriptableObject
{
    [SerializeField]
    public List<NotificationPrefab> notificationPrefabs;
}

/// <summary>
/// 提醒通知预制体
/// </summary>
[System.Serializable]
public class NotificationPrefab
{
    public NotificationType type;
    public GameObject prefab;
}
