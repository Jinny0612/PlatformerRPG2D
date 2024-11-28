using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ui管理
/// </summary>
public class UIManager : SingletonMonoBehvior<UIManager>
{
    /// <summary>
    /// 左上角玩家状态栏
    /// </summary>
    public PlayerStatusBar playerStatusBar;
    /// <summary>
    /// 输入控制
    /// </summary>
    private PlayerInputControls inputControl;
    
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

    [Header("菜单UI控制")]
    /// <summary>
    /// 菜单
    /// </summary>
    public MenuBar menuBar;
    /// <summary>
    /// 背包
    /// </summary>
    public GameObject backpackUI;
    /// <summary>
    /// 当前打开的菜单
    /// </summary>
    public GameObject currentOpenBar;

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

        inputControl = InputControlManager.Instance.InputControl;
        
        InitializedNotificationPrefabDictionary();

        inputControl = InputControlManager.Instance.InputControl;

        // 绑定打开背包操作
        inputControl.UI.OpenBag.started += OpenBackpacUI;
        // 绑定按下esc按键操作
        inputControl.UI.CloseMenu.started += OnPressEscapeButton;
    }

    private void OnEnable()
    {
        inputControl.Enable();

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
        inputControl.Disable();

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
    public void OnHealthEvent(Character character)
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

        if (notificationQueue.Count > 0)
        {
            //出队
            notificationQueue.Dequeue();

        }

        //删除字典中的元素
        notificationDictionary.Remove(notification.GetComponent<ItemNotification>().itemCode);
    }


    #region 游戏内菜单相关，比如查看背包、角色信息、设置等

    /// <summary>
    /// 设置UI菜单控制状态
    /// </summary>
    /// <param name="state"></param>
    public void SetInputControlUIState(bool state)
    {
        if (state)
        {
            inputControl.UI.Enable();
        }
        else
        {
            inputControl.UI.Disable();
        }
    }

    /// <summary>
    /// 变更MenuBar菜单的状态
    /// </summary>
    /// <param name="state"></param>
    private void SwitchMenuBarState(bool state)
    {
        menuBar.gameObject.SetActive(state);
    }

    /// <summary>
    /// 变更背包UI状态
    /// </summary>
    /// <param name="state"></param>
    private void SwitchBackpackUIState(bool state)
    {
        backpackUI.SetActive(state);
    }

    /// <summary>
    /// 打开背包
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OpenBackpacUI(InputAction.CallbackContext obj)
    {
        if (backpackUI.gameObject.activeInHierarchy && currentOpenBar)
        {
            // 背包已经打开了，按下按键就关闭背包
            CloseMenuBar(obj);
            currentOpenBar = null;
            // 此时可以打开玩家控制
            InputControlManager.Instance.EnableGameplayControl();
        }
        else
        {
            // 背包没打开，打开背包
            // 首先关闭玩家控制
            InputControlManager.Instance.DisableGameplayControl();
            // 先激活菜单
            SwitchMenuBarState(true);
            // 激活背包
            SwitchBackpackUIState(true);

            currentOpenBar = backpackUI.gameObject;

            // 显示背包内的物品
            EventHandler.CallInventoryUpdateEvent(InventoryLocation.player, 
                InventoryManager.Instance.GetInventoryListByInventoryType(InventoryLocation.player));
        }

    }

    /// <summary>
    /// 更新库存 主要用于打开背包后的操作
    /// </summary>
    /// <param name="location"></param>
    /// <param name="inventoryItemList"></param>
    private void UpdateInventory(InventoryLocation location,List<InventoryItem> inventoryItemList)
    {

    }

    private void SwitchMenuByMenuType()
    {

    }

    

    /// <summary>
    /// 关闭菜单
    /// </summary>
    /// <param name="obj"></param>
    private void CloseMenuBar(InputAction.CallbackContext obj)
    {
        SwitchMenuBarState(false);
        SwitchBackpackUIState(false);
    }

    /// <summary>
    /// 按下esc键触发
    /// </summary>
    /// <param name="obj"></param>
    private void OnPressEscapeButton(InputAction.CallbackContext obj)
    {
        if (menuBar.gameObject.activeInHierarchy)
        {
            // 菜单被激活，按下esc执行关闭菜单操作
            CloseMenuBar(obj);
            InputControlManager.Instance.EnableGameplayControl();
        }
        else
        {
            // 未激活菜单，此时按下esc打开暂停游戏界面
            InputControlManager.Instance.DisableGameplayControl();
        }
    }

    #endregion

}
