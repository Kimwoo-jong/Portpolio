using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : BaseItemSlot, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
	public event Action<ItemSlot> OnBeginDragEvent;
	public event Action<ItemSlot> OnEndDragEvent;
	public event Action<ItemSlot> OnDragEvent;
	public event Action<ItemSlot> OnDropEvent;

	private bool isDragging;

	public override bool CanAddStack(Item item, int amount = 1)
	{
		return base.CanAddStack(item, amount) && Amount + amount <= item.MaximumStacks;
	}

	public override bool CanReceiveItem(Item item)
	{
		return true;
	}

	protected override void OnDisable()
	{
		base.OnDisable();

		if (isDragging)
		{
			OnEndDrag(null);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		isDragging = true;

		if (Item != null)
			image.color = normalColor;

		if (OnBeginDragEvent != null)
			OnBeginDragEvent(this);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		isDragging = false;

		if (Item != null)
			image.color = normalColor;

		if (OnEndDragEvent != null)
			OnEndDragEvent(this);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (OnDragEvent != null)
			OnDragEvent(this);
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (OnDropEvent != null)
			OnDropEvent(this);
	}
}