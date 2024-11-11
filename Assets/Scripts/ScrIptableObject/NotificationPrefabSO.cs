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
/// ����֪ͨԤ����
/// </summary>
[System.Serializable]
public class NotificationPrefab
{
    public NotificationType type;
    public GameObject prefab;
}
