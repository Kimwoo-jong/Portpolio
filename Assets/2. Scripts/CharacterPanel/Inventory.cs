using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour, IItemContainer
{
    [FormerlySerializedAs("items")]
    [SerializeField] List<Item> startingItems;
    [SerializeField] Transform slotParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnPointerEnterEvent += slot=> OnPointerEnterEvent(slot);
            itemSlots[i].OnPointerExitEvent += slot => OnPointerExitEvent(slot);
            itemSlots[i].OnRightClickEvent += slot => OnRightClickEvent(slot);
            itemSlots[i].OnBeginDragEvent += slot => OnBeginDragEvent(slot);
            itemSlots[i].OnDragEvent += slot => OnDragEvent(slot);
            itemSlots[i].OnEndDragEvent += slot => OnEndDragEvent(slot);
            itemSlots[i].OnDropEvent += slot => OnDropEvent(slot);
        }

        SetStartingItems();
    }
    private void OnValidate()
    {
        if(slotParent != null)
        {
            itemSlots = slotParent.GetComponentsInChildren<ItemSlot>();
        }

        SetStartingItems();
    }
    public void SetStartingItems()
    {
        Clear();
        for (int i = 0; i < startingItems.Count; i++)
        {
            AddItem(startingItems[i]);
        }
    }
    public bool AddItem(Item _item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
            {
                itemSlots[i].Item = _item;
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(Item _item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == _item)
            {
                itemSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                itemSlots[i].Item = null;
                return item;
            }
        }
        return null;
    }
    public bool IsFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }

    public bool containsItem(Item item)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item)
            {
                return true;
            }
        }
        return false;
    }
    public void Clear()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
}