using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ui管理
/// </summary>
public class UIManager : SingletonMonoBehvior<UIManager>
{
    /// <summary>
    /// 左上角玩家状态栏
    /// </summary>
    public PlayerStatusBar playerStatusBar;
    
    [Header("拾取提醒列表")]
    /// <summary>
    /// 左下角拾取物品提醒父物品
    /// </summary>
    public Transform notificationParentTransform;
    /// <summary>
    /// 提醒队列
    /// </summary>
    public Queue<GameObject> notificationQueue = new Queue<GameObject>();
    /// <summary>
    /// k- itemcode v-提醒UI预制体
    /// </summary>
    private Dictionary<int,GameObject> notificationDictionary = new Dictionary<int,GameObject>();
    /// <summary>
    /// 队列最大长度
    /// </summary>
    public int maxLength;
    /// <summary>
    /// 提醒预制体配置信息
    /// </summary>
    public NotificationPrefabSO notificationPrefabSO;
    /// <summary>
    /// 提醒通知预制体类型字典
    /// </summary>
    private Dictionary<NotificationType,GameObject> notificationPrefabDict = new Dictionary<NotificationType,GameObject>();

    //[Header("事件监听")]
    /// <summary>
    /// 血量变化事件监听   Character绑定了OnHealthChange事件
    /// </summary>
    //public CharacterEventSO healthEvent;
    /// <summary>
    /// 场景加载事件监听
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
    /// 初始化预制体字典
    /// </summary>
    private void InitializedNotificationPrefabDictionary()
    {
        notificationDictionary.Clear();
        if(notificationPrefabSO == null)
        {
            Debug.Log("暂未配置通知提醒预制体信息");
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
    /// 根据通知类型获取对应预制体
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
    /// 设置player状态ui是否显示
    /// </summary>
    private void SetPlayerStatusBar(GameSceneSO sceneToLoad)
    {
        switch (sceneToLoad.sceneType)
        {
            case SceneType.Menu:
                // menu界面不显示
                playerStatusBar.gameObject.SetActive(false);
                break;
            case SceneType.Location: 
                playerStatusBar.gameObject.SetActive(true); 
                break;
            default: break;
        }

    }

    /// <summary>
    /// 血量变化事件
    /// </summary>
    /// <param name="character"></param>
    private void OnHealthEvent(Character character)
    {
        // 计算百分比
        float persentage = character.currentHealth / character.maxHealth;
        // 发送百分比

        // player血量变化
        playerStatusBar.OnHealthChange(persentage);

    }

    /// <summary>
    /// 玩家等级变化
    /// </summary>
    /// <param name="lvl"></param>
    private void OnPlayerLvlChangeEvent(int lvl)
    {
        playerStatusBar.SetPlayerLevel(lvl);
    }

    /// <summary>
    /// 玩家金币数量变化
    /// </summary>
    /// <param name="goldCoin"></param>
    private void OnPlayerGoldCoinChangeEvent(int goldCoin)
    {
        playerStatusBar.SetPlayerCoin(goldCoin);
    }

    /// <summary>
    /// 创建拾取提醒
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
