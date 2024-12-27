using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ������
/// </summary>
public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    /// <summary>
    /// �����
    /// </summary>
    private Camera mainCamera;
    /// <summary>
    /// ���໭��
    /// </summary>
    private Canvas parentCanvas;
    /// <summary>
    /// ����ĸ���
    /// </summary>
    private Transform parentItem;
    /// <summary>
    /// ����ק������
    /// </summary>
    private GameObject draggedItem;

    private PlayerInputControls inputControl;

    [Header("��ʾ�Ĳ���")]
    /// <summary>
    /// ѡ��ʱ�ĸ߹���ʾ
    /// </summary>
    public Image highlight;
    /// <summary>
    /// ��ǰ�����ƷͼƬ
    /// </summary>
    [HideInInspector] public Image inventoryItemImage;
    /// <summary>
    /// ��Ʒ�����ı���ʾ����
    /// </summary>
    public TextMeshProUGUI itemQuantityText;

    [Header("UI��Ԥ����")]
    /// <summary>
    /// ������  ���屻��קʱ��ʱ���������Ϸ������
    /// </summary>
    [SerializeField] private BackpackUI backpackUI;
    /// <summary>
    /// ��ƷԤ����
    /// </summary>
    [SerializeField] private GameObject itemPrefab;
    /// <summary>
    /// ��ʾ��Ʒ������ı���
    /// </summary>
    [SerializeField] private GameObject inventoryTextBoxPrefab;

    /// <summary>
    /// ��������λ�ã���Ҫ���ڽ�����Ʒʱʹ�ã�Ϊ�ɿ����ʱ�����ָ��ı�������
    /// </summary>
    [SerializeField] private int slotNumber = 0;

    /// <summary>
    /// ��Ʒ����
    /// </summary>
    [HideInInspector] public ItemDetails itemDetails;
    /// <summary>
    /// ��Ʒ��������ֵ
    /// </summary>
    [HideInInspector] public int itemQuantity;

    private void Awake()
    {
        mainCamera = Camera.main;
        parentCanvas = GetComponentInParent<Canvas>();
        inputControl = InputControlManager.Instance.InputControl;
    }

    private void Start()
    {
        parentItem = GameObject.FindGameObjectWithTag(Tags.DROPITEMPARENT).transform;
    }

    /// <summary>
    /// ��ʼ��ק
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if(itemDetails != null)
        {
            // ����һ��������ק������

            // ʵ������ק������,������Ϊ������
            draggedItem = Instantiate(backpackUI.inventoryDraggedItem, backpackUI.transform);

            // ������ק�������imageΪ��ǰ�����ӵ�����ͼƬ
            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = inventoryItemImage.sprite;
            //Debug.Log("ѡ������: " + itemDetails.itemDescription);

        }
    }

    /// <summary>
    /// ��ק������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if(draggedItem != null)
        {
            // ��������ƶ�
            draggedItem.transform.position = inputControl.UI.Point.ReadValue<Vector2>();
        }
    }

    /// <summary>
    /// ������ק
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if(draggedItem != null)
        {
            // ������ק������
            Destroy(draggedItem);

            if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != null)
            {
                // �������ʱ���λ���ڱ������ĸ�����
                // ��ȡ��ǰ�����ָ�ı������ӱ��
                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>().slotNumber;

                // ������Ʒλ��
                InventoryManager.Instance.SwapInventoryItems(InventoryLocation.player, slotNumber, toSlotNumber);

                DestroyInventoryTextBox();
            }
            else
            {
                // �������Ƴ��˱�����
                if (itemDetails.canBeDropped)
                {
                    // ��Ʒ��������
                    DropSelectedItem();
                }

            }
            
        }
    }

    /// <summary>
    /// ���ñ������ӱ��
    /// </summary>
    /// <param name="slotNumber"></param>
    public void SetSlotNumber(int slotNumber)
    {
        this.slotNumber = slotNumber;
    }

    

    /// <summary>
    /// ����ѡ����Ʒ
    /// </summary>
    private void DropSelectedItem()
    {
        InventoryManager.Instance.RemoveItem(InventoryLocation.player, itemDetails.itemCode);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // todo: �����Ƿ����ͨ������ع������ⲻ�Ͻ�������Ĵ��������٣����ȼ��ϵͣ����Ժ����ٿ���
        if(itemQuantity != 0)
        {
            // ��Ʒ������Ϊ0,����ǰ����λ������Ʒ �Ż���д���

            //ʵ������ʾ����Ʒ�����ı�
            //todo: λ�ò��ԣ�����пյ�ʱ���ٿ���ʲôԭ�����ȼ��ǳ���
            backpackUI.inventoryTextBoxGameobject = Instantiate(inventoryTextBoxPrefab, transform.position, Quaternion.identity);
            backpackUI.inventoryTextBoxGameobject.transform.SetParent(transform, false);

            UIInventoryTextBox inventoryTextBox = backpackUI.inventoryTextBoxGameobject.GetComponent<UIInventoryTextBox>();

            // ������ʾ����Ϣ
            inventoryTextBox.ShowInventoryTextBox(itemDetails);

            // ������Ʒ������λ��
            RectTransform rectTransform = backpackUI.inventoryTextBoxGameobject.GetComponent<RectTransform>();
            // λ���޸ĵ��������
            Vector2 mousePosition = eventData.position;
            rectTransform.anchoredPosition = mousePosition;


        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyInventoryTextBox();
    }

    public void DestroyInventoryTextBox()
    {
        if(backpackUI.inventoryTextBoxGameobject != null)
        {
            Destroy(backpackUI.inventoryTextBoxGameobject);
        }
    }
}
