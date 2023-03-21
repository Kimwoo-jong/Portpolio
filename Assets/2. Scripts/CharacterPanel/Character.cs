using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //아래의 스탯값을 기본값으로 Percent 연산을 진행한다.
    [Header("Player Stats")]
    public CharacterStat damage;
    public CharacterStat defense;
    public CharacterStat tough;
    public CharacterStat block;
    public CharacterStat critical;
    public CharacterStat criDamage;
    public CharacterStat evade;
    public CharacterStat moveSpeed;
    public CharacterStat atkSpeed;
    public CharacterStat reloadSpeed;
    public CharacterStat dashDamage;
    public CharacterStat trueDamage;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemToolTip itemToolTip;
    [SerializeField] Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] QuestionDialog questionDialog;

    private ItemSlot draggedItemSlot;

    private void OnValidate()
    {
        if (itemToolTip == null)
        {
            itemToolTip = FindObjectOfType<ItemToolTip>();
        }
    }

    private void Start()
    {
        statPanel.SetStats(damage, defense, tough, block, critical, criDamage,
            evade, moveSpeed, atkSpeed, reloadSpeed, dashDamage, trueDamage);
        statPanel.UpdateStatValues();

        #region 이벤트 설정
        //우클릭
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += UnEquip;
        //포인터 Enter
        inventory.OnPointerEnterEvent += ShowTooltip;
        equipmentPanel.OnPointerEnterEvent += ShowTooltip;
        //포인터 Exit
        inventory.OnPointerExitEvent += HideTooltip;
        equipmentPanel.OnPointerExitEvent += HideTooltip;
        //드래그 시작
        inventory.OnBeginDragEvent += BeginDrag;
        equipmentPanel.OnBeginDragEvent += BeginDrag;
        //드래그 중
        inventory.OnDragEvent += Drag;
        equipmentPanel.OnDragEvent += Drag;
        //드래그 종료
        inventory.OnEndDragEvent += EndDrag;
        equipmentPanel.OnEndDragEvent += EndDrag;
        //드롭
        inventory.OnDropEvent += Drop;
        equipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;
        #endregion
    }
    private void Equip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;

        if (equippableItem != null)
        {
            Equip(equippableItem);
        }
    }
    private void UnEquip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;

        if (equippableItem != null)
        {
            UnEquip(equippableItem);
        }
    }
    private void ShowTooltip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;

        if (equippableItem != null)
        {
            itemToolTip.ShowToolTip(equippableItem);
        }
    }
    private void HideTooltip(ItemSlot itemSlot)
    {
        itemToolTip.HideToolTip();
    }
    private void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot != null)
        {
            draggedItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.itemImage;
            draggableItem.color = new Color(1, 1, 1, 0.75f);
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }
    private void EndDrag(ItemSlot itemSlot)
    {
        draggedItemSlot = null;
        draggableItem.enabled = false;
    }
    private void Drag(ItemSlot itemSlot)
    {
        if(draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }
    private void Drop(ItemSlot dropItemSlot)
    {
        if(draggedItemSlot == null)
            return;

        if(dropItemSlot.CanReceiveItem(draggedItemSlot.Item) && draggedItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            EquippableItem dragItem = draggedItemSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(draggedItemSlot is EquipmentSlot)
            {
                if (dragItem != null)
                    dragItem.UnEquip(this);

                if (dropItem != null)
                    dropItem.Equip(this);
            }
            if (dropItemSlot is EquipmentSlot)
            {
                if (dragItem != null)
                    dragItem.Equip(this);

                if (dropItem != null)
                    dropItem.UnEquip(this);
            }
            statPanel.UpdateStatValues();

            Item draggedItem = draggedItemSlot.Item;
            draggedItemSlot.Item = dropItemSlot.Item;
            dropItemSlot.Item = draggedItem;
        }
    }
    private void DropItemOutsideUI()
    {
        if (draggedItemSlot == null)
            return;

        questionDialog.Show();
        ItemSlot itemSlot = draggedItemSlot;
        questionDialog.OnYesEvent += () => DestroyItemInSlot(itemSlot);
    }
    private void DestroyItemInSlot(ItemSlot itemSlot)
    {
        if (itemSlot is EquipmentSlot)
        {
            EquippableItem equippableItem = (EquippableItem)itemSlot.Item;
            equippableItem.UnEquip(this);
        }

        itemSlot.Item.Destroy();
        itemSlot.Item = null;
    }
    private void EquipFromInventory(Item item)
    {
        if (item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }
    private void UnEquipFromEquipPanel(Item item)
    {
        if (item is EquippableItem)
        {
            UnEquip((EquippableItem)item);
        }
    }
    public void Equip(EquippableItem item)
    {
        if (inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (equipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.UnEquip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }
    public void UnEquip(EquippableItem item)
    {
        if (!inventory.IsFull() && equipmentPanel.RemoveItem(item))
        {
            item.UnEquip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
}