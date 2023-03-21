using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemContainer
{
    bool containsItem(Item item);
    Item RemoveItem(string itemID);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool IsFull();
    void Clear();
}