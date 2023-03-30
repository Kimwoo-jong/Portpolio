using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] protected Image image;

	public event Action<BaseItemSlot> OnPointerEnterEvent;
	public event Action<BaseItemSlot> OnPointerExitEvent;
	public event Action<BaseItemSlot> OnRightClickEvent;

	protected bool isPointerOver;

	protected Color normalColor = Color.white;
	protected Color disabledColor = new Color(1, 1, 1, 0);

	protected Item _item;
	public Item Item
	{
		get { return _item; }
		set
		{
			_item = value;

			if (_item == null)
			{
				image.sprite = null;
				image.color = disabledColor;
			}
			else
			{
				image.sprite = _item.itemImage;
				image.color = normalColor;
			}

			if (isPointerOver)
			{
				OnPointerExit(null);
				OnPointerEnter(null);
			}
		}
	}

	public virtual bool CanAddStack(Item item)
	{
		return Item != null && Item.ID == item.ID;
	}

	public virtual bool CanReceiveItem(Item item)
	{
		return false;
	}

	protected virtual void OnValidate()
	{
		if (image == null)
			image = GetComponent<Image>();

		Item = _item;
	}

	protected virtual void OnDisable()
	{
		if (isPointerOver)
		{
			OnPointerExit(null);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
		{
			if (OnRightClickEvent != null)
				OnRightClickEvent(this);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerOver = true;

		if (OnPointerEnterEvent != null)
			OnPointerEnterEvent(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerOver = false;

		if (OnPointerExitEvent != null)
			OnPointerExitEvent(this);
	}
}