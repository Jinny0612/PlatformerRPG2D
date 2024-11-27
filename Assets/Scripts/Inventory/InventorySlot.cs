using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 库存格子
/// </summary>
public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    /// <summary>
    /// 主相机
    /// </summary>
    private Camera mainCamera;
    /// <summary>
    /// 物体的父类
    /// </summary>
    private Transform parentItem;
    /// <summary>
    /// 被拖拽的物体
    /// </summary>
    private GameObject draggedItem;

    private PlayerInputControls inputControl;

    [Header("显示的参数")]
    /// <summary>
    /// 选中时的高光提示
    /// </summary>
    public Image highlight;
    /// <summary>
    /// 当前插槽物品图片
    /// </summary>
    [HideInInspector] public Image inventoryItemImage;
    /// <summary>
    /// 物品数量文本显示内容
    /// </summary>
    public TextMeshProUGUI itemQuantityText;

    [Header("UI和预制体")]
    /// <summary>
    /// 背包栏  物体被拖拽时暂时放在这个游戏物体下
    /// </summary>
    [SerializeField] private BackpackUI backpackUI;
    [SerializeField] private GameObject itemPrefab;


    /// <summary>
    /// 物品详情
    /// </summary>
    /*[HideInInspector] */public ItemDetails itemDetails;
    /// <summary>
    /// 物品数量，数值
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
    /// 开始拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if(itemDetails != null)
        {
            // 这是一个可以拖拽的物体

            // 实例化拖拽的物体,父物体为背包栏
            draggedItem = Instantiate(backpackUI.inventoryDraggedItem, backpackUI.transform);

            // 设置拖拽的物体的image为当前库存格子的物体图片
            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = inventoryItemImage.sprite;

            //Debug.Log("选中物体: " + itemDetails.itemDescription);
            
        }
    }

    /// <summary>
    /// 拖拽过程中
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if(draggedItem != null)
        {
            // 跟随鼠标移动
            draggedItem.transform.position = inputControl.UI.Point.ReadValue<Vector2>();
        }
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if(draggedItem != null)
        {
            // 销毁拖拽的物体
            Destroy(draggedItem);

            if(eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != null)
            {
                // todo:如果结束时鼠标位置在背包栏的格子上
            }
            else
            {
                // 将物体移出了背包栏
                if (itemDetails.canBeDropped)
                {
                    // 物品数量减少
                    DropSelectedItem();
                }

            }
            
        }
    }

    

    /// <summary>
    /// 丢弃选中物品
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
