using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler,
                                        IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] Image itemImage;

    #region 마우스 이벤트
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;
    #endregion

    protected bool isPointerOver;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private Item _item;
    public Item Item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;

            if (_item == null)
            {
                itemImage.color = disabledColor;
            }
            else
            {
                itemImage.sprite = Item.itemImage;
                itemImage.color = normalColor;
            }
            if(isPointerOver)
            {
                OnPointerEnter(null);
                OnPointerExit(null);
            }
        }
    }
    protected virtual void OnValidate()
    {
        if (itemImage == null)
        {
            itemImage = GetComponent<Image>();
        }
    }
    protected virtual void OnDisable()
    {
        if(isPointerOver)
        {
            OnPointerExit(null);
        }
    }
    public void Destroy()
    {
        Destroy(this);
    }
    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }
    //마우스 클릭 이벤트
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }
    //마우스 포인터가 올라갔을 때

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOver = true;

        if (OnPointerEnterEvent != null)
        {
            OnPointerEnterEvent(this);
        }
    }
    //마우스 포인터가 나왔을 때

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;

        if (OnPointerExitEvent != null)
        {
            OnPointerExitEvent(this);
        }
    }
    //마우스 드래그 시작

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnBeginDragEvent != null)
        {
            OnBeginDragEvent(this);
        }
    }
    //마우스 드래그 중

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(this);
        }
    }
    //마우스 드래그 끝났을 때

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null)
        {
            OnEndDragEvent(this);
        }
    }
    //마우스를 놓았을 때

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null)
        {
            OnDropEvent(this);
        }
    }
}
