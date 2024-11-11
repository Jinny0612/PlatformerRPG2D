using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ui����
/// </summary>
public class UIManager : SingletonMonoBehvior<UIManager>
{
    /// <summary>
    /// ���Ͻ����״̬��
    /// </summary>
    public PlayerStatusBar playerStatusBar;
    
    [Header("ʰȡ�����б�")]
    /// <summary>
    /// ���½�ʰȡ��Ʒ���Ѹ���Ʒ
    /// </summary>
    public Transform notificationParentTransform;
    /// <summary>
    /// ���Ѷ���
    /// </summary>
    public Queue<GameObject> notificationQueue;
    /// <summary>
    /// k- itemcode v-����UIԤ����
    /// </summary>
    private Dictionary<int,GameObject> notificationDictionary;
    /// <summary>
    /// ������󳤶�
    /// </summary>
    public int maxLength;
    /// <summary>
    /// ����Ԥ����������Ϣ
    /// </summary>
    public NotificationPrefabSO notificationPrefabSO;
    /// <summary>
    /// ����֪ͨԤ���������ֵ�
    /// </summary>
    private Dictionary<NotificationType,GameObject> notificationPrefabDict;

    //[Header("�¼�����")]
    /// <summary>
    /// Ѫ���仯�¼�����   Character����OnHealthChange�¼�
    /// </summary>
    //public CharacterEventSO healthEvent;
    /// <summary>
    /// ���������¼�����
    /// </summary>
    //public SceneLoadEventSO sceneLoadEvent;

    protected override void Awake()
    {
        base.Awake();

        notificationQueue = new Queue<GameObject>();
        notificationDictionary = new Dictionary<int, GameObject>();
        notificationPrefabDict = new Dictionary<NotificationType, GameObject>();

        InitializedNotificationPrefabDictionary();
    }

    private void OnEnable()
    {
        //healthEvent.OnEventCalled += OnHealthEvent;
        EventHandler.OnHealthChangeEvent += OnHealthEvent;

        //sceneLoadEvent.LoadRequestEvent += SetPlayerStatusBar;
        EventHandler.OnSetPlayerStatusBarEvent += SetPlayerStatusBar;

        EventHandler.OnPlayerLevelChangeEvent += OnPlayerLvlChangeEvent;
        EventHandler.OnPlayerGoldCoinChangeEvent += OnPlayerGoldCoinChangeEvent;

        EventHandler.OnCreateNotificationEvent += CreateNotification;
        EventHandler.OnDeleteNotificationAfterTimeCount += DeleteNotification;
    }

    private void OnDisable()
    {
        //healthEvent.OnEventCalled -= OnHealthEvent;
        EventHandler.OnHealthChangeEvent -= OnHealthEvent;


        //sceneLoadEvent.LoadRequestEvent -= SetPlayerStatusBar;
        EventHandler.OnSetPlayerStatusBarEvent -= SetPlayerStatusBar;

        EventHandler.OnPlayerLevelChangeEvent -= OnPlayerLvlChangeEvent;
        EventHandler.OnPlayerGoldCoinChangeEvent -= OnPlayerGoldCoinChangeEvent;

        EventHandler.OnCreateNotificationEvent -= CreateNotification;
        EventHandler.OnDeleteNotificationAfterTimeCount -= DeleteNotification;
    }

    /// <summary>
    /// ��ʼ��Ԥ�����ֵ�
    /// </summary>
    private void InitializedNotificationPrefabDictionary()
    {
        notificationDictionary.Clear();
        if(notificationPrefabSO == null)
        {
            Debug.Log("��δ����֪ͨ����Ԥ������Ϣ");
            return;
        }

        if(notificationDictionary.Count == 0)
        {
            foreach(NotificationPrefab info in notificationPrefabSO.notificationPrefabs)
            {
                notificationPrefabDict.Add(info.type, info.prefab);
            }
        }
    }

    /// <summary>
    /// ����֪ͨ���ͻ�ȡ��ӦԤ����
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private GameObject GetNotificationPrefabByType(NotificationType type)
    {
        if(notificationPrefabDict.Count == 0)
        {
            InitializedNotificationPrefabDictionary();
        }

        return notificationPrefabDict[type];
    }

    /// <summary>
    /// ����player״̬ui�Ƿ���ʾ
    /// </summary>
    private void SetPlayerStatusBar(GameSceneSO sceneToLoad)
    {
        switch (sceneToLoad.sceneType)
        {
            case SceneType.Menu:
                // menu���治��ʾ
                playerStatusBar.gameObject.SetActive(false);
                break;
            case SceneType.Location: 
                playerStatusBar.gameObject.SetActive(true); 
                break;
            default: break;
        }

    }

    /// <summary>
    /// Ѫ���仯�¼�
    /// </summary>
    /// <param name="character"></param>
    private void OnHealthEvent(Character character)
    {
        // ����ٷֱ�
        float persentage = character.currentHealth / character.maxHealth;
        // ���Ͱٷֱ�

        // playerѪ���仯
        playerStatusBar.OnHealthChange(persentage);

    }

    /// <summary>
    /// ��ҵȼ��仯
    /// </summary>
    /// <param name="lvl"></param>
    private void OnPlayerLvlChangeEvent(int lvl)
    {
        playerStatusBar.SetPlayerLevel(lvl);
    }

    /// <summary>
    /// ��ҽ�������仯
    /// </summary>
    /// <param name="goldCoin"></param>
    private void OnPlayerGoldCoinChangeEvent(int goldCoin)
    {
        playerStatusBar.SetPlayerCoin(goldCoin);
    }

    /// <summary>
    /// ����ʰȡ����
    /// </summary>
    /// <param name="enemyInfo"></param>
    private void CreateNotification(NotificationType type,int num,int itemCode)
    {
        GameObject instance = null;
        ItemDetails itemDetail = InventoryManager.Instance.GetItemDetailByCode(itemCode);

        if (notificationDictionary.ContainsKey(itemCode))
        {
            // ��ǰ�����֪ͨ���ڣ�ȡ������֪ͨ
            GameObject firstGameObject = notificationQueue.First();
            ItemNotification firstNotification = firstGameObject.GetComponent<ItemNotification>();

            if (firstNotification != null && firstNotification.itemCode == itemCode)
            {
                // ��ǰ������Ϊ���ף����ӡ����������������β
                //����
                notificationQueue.Dequeue();
                instance = firstGameObject;
            }
            else
            {
                // �������У�����ȡ����Ӧ��Ԫ�أ�������Ϣ������β
                List<GameObject> tempList = notificationQueue.ToList<GameObject>();
                foreach(GameObject item in notificationQueue)
                {
                    ItemNotification info = item.GetComponent<ItemNotification>();
                    if(info.itemCode == itemCode)
                    {
                        // ȡ����Ӧ��Ԫ��
                        instance = item;
                        tempList.Remove(item);
                    }
                }
                // ���¶��У���������itemCode��ͬ��Ԫ��
                notificationQueue.Clear();
                notificationQueue = new Queue<GameObject>(tempList);
                
            }
        }
        else
        {
            GameObject prefab = GetNotificationPrefabByType(type);
            instance = Instantiate(prefab, notificationParentTransform);

        }

        // ���������������β
        if (instance != null)
        {
            ItemNotification itemNotification = instance.GetComponent<ItemNotification>();

            itemNotification.SetItemNotification(num, itemDetail.itemSprite, itemDetail.itemDescription);

            if (notificationQueue.Count == maxLength)
            {
                // �������ˣ����׳���
                GameObject removeGameObject = notificationQueue.Dequeue();
                notificationDictionary.Remove(removeGameObject.GetComponent<ItemNotification>().itemCode);
            }
            // �������
            notificationQueue.Enqueue(instance);
            notificationDictionary[itemCode] = instance;

        }
    }

    /// <summary>
    /// ɾ��֪ͨ  ����֪ͨ��ʾ����ʱ��ʱ��ʹ��,һ����ɾ������Ԫ��
    /// </summary>
    /// <param name="notification"></param>
    private void DeleteNotification(GameObject notification)
    {
        //����
        notificationQueue.Dequeue();

        //ɾ���ֵ��е�Ԫ��
        notificationDictionary.Remove(notification.GetComponent<ItemNotification>().itemCode);
    }

}
