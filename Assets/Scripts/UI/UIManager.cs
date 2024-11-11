using System;
using System.Collections;
using System.Collections.Generic;
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
    public Queue<GameObject> notificationQueue = new Queue<GameObject>();
    /// <summary>
    /// k- itemcode v-����UIԤ����
    /// </summary>
    private Dictionary<int,GameObject> notificationDictionary = new Dictionary<int,GameObject>();
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
    private Dictionary<NotificationType,GameObject> notificationPrefabDict = new Dictionary<NotificationType,GameObject>();

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
        GameObject prefab = GetNotificationPrefabByType(type);
        GameObject instance = Instantiate(prefab, notificationParentTransform);
        if(instance != null)
        {
            ItemNotification itemNotification = instance.GetComponent<ItemNotification>();

            ItemDetails itemDetail = InventoryManager.Instance.GetItemDetailByCode(itemCode);

            itemNotification.SetItemNotification(num, itemDetail.itemSprite, itemDetail.itemDescription);
        }
    }

}
