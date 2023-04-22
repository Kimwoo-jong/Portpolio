using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("Public")]
    public Inventory Inventory;
    public EquipmentPanel EquipmentPanel;

    [Header("Serialize Field")]
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemToolTip itemTooltip;
    [SerializeField] Image draggableItem;
    [SerializeField] DropItemArea dropItemArea;
    [SerializeField] QuestionDialog reallyDropItemDialog;
    [SerializeField] ItemSaveManager itemSaveManager;

    private BaseItemSlot dragItemSlot;

    private void OnValidate()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemToolTip>();
    }

    private void Start()
    {
        //우클릭
        Inventory.OnRightClickEvent += InventoryRightClick;
        EquipmentPanel.OnRightClickEvent += EquipmentPanelRightClick;
        //마우스 포인터 올렸을 때
        Inventory.OnPointerEnterEvent += ShowTooltip;
        EquipmentPanel.OnPointerEnterEvent += ShowTooltip;
        //마우스 포인터 나갔을 때
        Inventory.OnPointerExitEvent += HideTooltip;
        EquipmentPanel.OnPointerExitEvent += HideTooltip;
        //드래그 시작
        Inventory.OnBeginDragEvent += BeginDrag;
        EquipmentPanel.OnBeginDragEvent += BeginDrag;
        //드래그 종료
        Inventory.OnEndDragEvent += EndDrag;
        EquipmentPanel.OnEndDragEvent += EndDrag;
        //드래그 중
        Inventory.OnDragEvent += Drag;
        EquipmentPanel.OnDragEvent += Drag;
        //드롭
        Inventory.OnDropEvent += Drop;
        EquipmentPanel.OnDropEvent += Drop;
        dropItemArea.OnDropEvent += DropItemOutsideUI;

        if(itemSaveManager != null)
        {
            itemSaveManager.LoadEquipment(this);
            itemSaveManager.LoadInventory(this);
        }
    }
    private void LateUpdate()
    {
        statPanel.UpdateStatNames();
        statPanel.UpdateStatDescription();
    }

    private void OnDestroy()
    {
        if(itemSaveManager != null)
        {
            itemSaveManager.SaveEquipment(this);
            itemSaveManager.SaveInventory(this);
        }
    }

    private void InventoryRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem)itemSlot.Item);
        }
    }

    private void EquipmentPanelRightClick(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            Unequip((EquippableItem)itemSlot.Item);
        }
    }

    private void ShowTooltip(BaseItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;
        if (equippableItem != null)
        {
            itemTooltip.ShowToolTip(equippableItem);
        }
    }

    private void HideTooltip(BaseItemSlot itemSlot)
    {
        if (itemTooltip.gameObject.activeSelf)
        {
            itemTooltip.HideToolTip();
        }
    }

    private void BeginDrag(BaseItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
        {
            dragItemSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.itemImage;
            draggableItem.color = new Color(1, 1, 1, 0.5f);
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.gameObject.SetActive(true);
        }
    }

    private void Drag(BaseItemSlot itemSlot)
    {
        draggableItem.transform.position = Input.mousePosition;
    }

    private void EndDrag(BaseItemSlot itemSlot)
    {
        dragItemSlot = null;
        draggableItem.gameObject.SetActive(false);
    }

    private void Drop(BaseItemSlot dropItemSlot)
    {
        if (dragItemSlot == null) return;

        else if (dropItemSlot.CanReceiveItem(dragItemSlot.Item) && dragItemSlot.CanReceiveItem(dropItemSlot.Item))
        {
            SwapItems(dropItemSlot);
        }
    }

    private void SwapItems(BaseItemSlot dropItemSlot)
    {
        EquippableItem dragEquipItem = dragItemSlot.Item as EquippableItem;
        EquippableItem dropEquipItem = dropItemSlot.Item as EquippableItem;

        if (dropItemSlot is EquipmentSlot)
        {
            if (dragEquipItem != null) dragEquipItem.Equip(PlayerStatus.playerStatus);
            if (dropEquipItem != null) dropEquipItem.UnEquip(PlayerStatus.playerStatus);
        }
        if (dragItemSlot is EquipmentSlot)
        {
            if (dragEquipItem != null) dragEquipItem.UnEquip(PlayerStatus.playerStatus);
            if (dropEquipItem != null) dropEquipItem.Equip(PlayerStatus.playerStatus);
        }
        statPanel.UpdateStatValues();

        Item draggedItem = dragItemSlot.Item;

        dragItemSlot.Item = dropItemSlot.Item;

        dropItemSlot.Item = draggedItem;
    }

    private void DropItemOutsideUI()
    {
        if (dragItemSlot == null) return;

        reallyDropItemDialog.Show();
        BaseItemSlot slot = dragItemSlot;
        reallyDropItemDialog.OnYesEvent += () => DestroyItemInSlot(slot);
    }

    private void DestroyItemInSlot(BaseItemSlot itemSlot)
    {
        // If the item is equiped, unequip first
        if (itemSlot is EquipmentSlot)
        {
            EquippableItem equippableItem = (EquippableItem)itemSlot.Item;
            equippableItem.UnEquip(PlayerStatus.playerStatus);
        }

        itemSlot.Item.Destroy();
        itemSlot.Item = null;
    }

    public void Equip(EquippableItem item)
    {
        if (Inventory.RemoveItem(item))
        {
            EquippableItem previousItem;
            if (EquipmentPanel.AddItem(item, out previousItem))
            {
                if (previousItem != null)
                {
                    Inventory.AddItem(previousItem);
                    previousItem.UnEquip(PlayerStatus.playerStatus);
                    statPanel.UpdateStatValues();
                }

                item.Equip(PlayerStatus.playerStatus);
                statPanel.UpdateStatValues();
            }
            else
            {
                Inventory.AddItem(item);
            }
        }
    }

    public void Unequip(EquippableItem item)
    {
        if (Inventory.CanAddItem(item) && EquipmentPanel.RemoveItem(item))
        {
            item.UnEquip(PlayerStatus.playerStatus);
            statPanel.UpdateStatValues();
            Inventory.AddItem(item);
        }
    }
}