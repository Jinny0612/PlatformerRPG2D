using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ui����
/// </summary>
public class UIManager : SingletonMonoBehvior<UIManager>
{
    /// <summary>
    /// ���Ͻ����״̬��
    /// </summary>
    public PlayerStatusBar playerStatusBar;
    /// <summary>
    /// �������
    /// </summary>
    private PlayerInputControls inputControl;
    
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

    [Header("�˵�UI����")]
    /// <summary>
    /// �˵�
    /// </summary>
    public MenuBar menuBar;
    /// <summary>
    /// ����
    /// </summary>
    public GameObject backpackUI;
    /// <summary>
    /// ��ǰ�򿪵Ĳ˵�
    /// </summary>
    public GameObject currentOpenBar;

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

        inputControl = InputControlManager.Instance.InputControl;
        
        InitializedNotificationPrefabDictionary();

        inputControl = InputControlManager.Instance.InputControl;

        // �󶨴򿪱�������
        inputControl.UI.OpenBag.started += OpenBackpacUI;
        // �󶨰���esc��������
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
    public void OnHealthEvent(Character character)
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

        if (notificationQueue.Count > 0)
        {
            //����
            notificationQueue.Dequeue();

        }

        //ɾ���ֵ��е�Ԫ��
        notificationDictionary.Remove(notification.GetComponent<ItemNotification>().itemCode);
    }


    #region ��Ϸ�ڲ˵���أ�����鿴��������ɫ��Ϣ�����õ�

    /// <summary>
    /// ����UI�˵�����״̬
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
    /// ���MenuBar�˵���״̬
    /// </summary>
    /// <param name="state"></param>
    private void SwitchMenuBarState(bool state)
    {
        menuBar.gameObject.SetActive(state);
    }

    /// <summary>
    /// �������UI״̬
    /// </summary>
    /// <param name="state"></param>
    private void SwitchBackpackUIState(bool state)
    {
        backpackUI.SetActive(state);
    }

    /// <summary>
    /// �򿪱���
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void OpenBackpacUI(InputAction.CallbackContext obj)
    {
        if (backpackUI.gameObject.activeInHierarchy && currentOpenBar)
        {
            // �����Ѿ����ˣ����°����͹رձ���
            CloseMenuBar(obj);
            currentOpenBar = null;
            // ��ʱ���Դ���ҿ���
            InputControlManager.Instance.EnableGameplayControl();
        }
        else
        {
            // ����û�򿪣��򿪱���
            // ���ȹر���ҿ���
            InputControlManager.Instance.DisableGameplayControl();
            // �ȼ���˵�
            SwitchMenuBarState(true);
            // �����
            SwitchBackpackUIState(true);

            currentOpenBar = backpackUI.gameObject;

            // ��ʾ�����ڵ���Ʒ
            EventHandler.CallInventoryUpdateEvent(InventoryLocation.player, 
                InventoryManager.Instance.GetInventoryListByInventoryType(InventoryLocation.player));
        }

    }

    /// <summary>
    /// ���¿�� ��Ҫ���ڴ򿪱�����Ĳ���
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
    /// �رղ˵�
    /// </summary>
    /// <param name="obj"></param>
    private void CloseMenuBar(InputAction.CallbackContext obj)
    {
        SwitchMenuBarState(false);
        SwitchBackpackUIState(false);
    }

    /// <summary>
    /// ����esc������
    /// </summary>
    /// <param name="obj"></param>
    private void OnPressEscapeButton(InputAction.CallbackContext obj)
    {
        if (menuBar.gameObject.activeInHierarchy)
        {
            // �˵����������escִ�йرղ˵�����
            CloseMenuBar(obj);
            InputControlManager.Instance.EnableGameplayControl();
        }
        else
        {
            // δ����˵�����ʱ����esc����ͣ��Ϸ����
            InputControlManager.Instance.DisableGameplayControl();
        }
    }

    #endregion

}
