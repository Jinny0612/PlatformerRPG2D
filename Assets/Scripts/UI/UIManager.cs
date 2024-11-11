using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Queue<GameObject> notificationQueue;
    /// <summary>
    /// k- itemcode v-提醒UI预制体
    /// </summary>
    private Dictionary<int,GameObject> notificationDictionary;
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
    private Dictionary<NotificationType,GameObject> notificationPrefabDict;

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
        GameObject instance = null;
        ItemDetails itemDetail = InventoryManager.Instance.GetItemDetailByCode(itemCode);

        if (notificationDictionary.ContainsKey(itemCode))
        {
            // 当前物体的通知存在，取出这条通知
            GameObject firstGameObject = notificationQueue.First();
            ItemNotification firstNotification = firstGameObject.GetComponent<ItemNotification>();

            if (firstNotification != null && firstNotification.itemCode == itemCode)
            {
                // 当前物体编号为队首，出队、更新数量、插入队尾
                //出队
                notificationQueue.Dequeue();
                instance = firstGameObject;
            }
            else
            {
                // 遍历队列，从中取出对应的元素，更新信息后插入队尾
                List<GameObject> tempList = notificationQueue.ToList<GameObject>();
                foreach(GameObject item in notificationQueue)
                {
                    ItemNotification info = item.GetComponent<ItemNotification>();
                    if(info.itemCode == itemCode)
                    {
                        // 取出对应的元素
                        instance = item;
                        tempList.Remove(item);
                    }
                }
                // 更新队列，不包含与itemCode相同的元素
                notificationQueue.Clear();
                notificationQueue = new Queue<GameObject>(tempList);
                
            }
        }
        else
        {
            GameObject prefab = GetNotificationPrefabByType(type);
            instance = Instantiate(prefab, notificationParentTransform);

        }

        // 更新数量、插入队尾
        if (instance != null)
        {
            ItemNotification itemNotification = instance.GetComponent<ItemNotification>();

            itemNotification.SetItemNotification(num, itemDetail.itemSprite, itemDetail.itemDescription);

            if (notificationQueue.Count == maxLength)
            {
                // 队列满了，队首出队
                GameObject removeGameObject = notificationQueue.Dequeue();
                notificationDictionary.Remove(removeGameObject.GetComponent<ItemNotification>().itemCode);
            }
            // 插入队列
            notificationQueue.Enqueue(instance);
            notificationDictionary[itemCode] = instance;

        }
    }

    /// <summary>
    /// 删除通知  用于通知显示倒计时的时候使用,一般是删除队首元素
    /// </summary>
    /// <param name="notification"></param>
    private void DeleteNotification(GameObject notification)
    {
        //出队
        notificationQueue.Dequeue();

        //删除字典中的元素
        notificationDictionary.Remove(notification.GetComponent<ItemNotification>().itemCode);
    }

}
