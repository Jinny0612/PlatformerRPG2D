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
    [SerializeField] private GameObject itemPrefab;


    /// <summary>
    /// ��Ʒ����
    /// </summary>
    /*[HideInInspector] */public ItemDetails itemDetails;
    /// <summary>
    /// ��Ʒ��������ֵ
    /// </summary>
    [HideInInspector] public int itemQuantity;

    private void Awake()
    {
        mainCamera = Camera.main;
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
                // todo:�������ʱ���λ���ڱ������ĸ�����
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
    /// ����ѡ����Ʒ
    /// </summary>
    private void DropSelectedItem()
    {
        InventoryManager.Instance.RemoveItem(InventoryLocation.player, itemDetails.itemCode);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(this.gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
