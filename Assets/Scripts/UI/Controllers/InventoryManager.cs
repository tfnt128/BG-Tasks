using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryManager : MonoBehaviour
{
    [SerializeField] protected UIInventory uiInventory;
    [SerializeField] protected InventorySO inventoryData;

    protected int equippedItemIndex = -1;

    protected virtual void Start()
    {
        if (inventoryData == null)
        {
            return;
        }

        inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        EquipmentEvents.OnItemEquipped += HandleItemEquipped;
        EquipmentEvents.OnItemUnequipped += HandleItemUnequipped;
        UpdateUI();
    }

    protected void UpdateUI()
    {
        uiInventory.InitializeInventoryUI(inventoryData.Size);
        uiInventory.OnDescriptionRequested += HandleDescriptionRequest;
        uiInventory.OnItemActionRequested += HandleItemActionRequest;
    }

    protected void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        foreach (var item in inventoryState)
        {
            uiInventory.UpdateData(item.Key, item.Value.item.ItemImage);
        }

        for (int i = 0; i < inventoryData.Size; i++)
        {
            if (!inventoryState.ContainsKey(i))
            {
                uiInventory.ResetData(i);
            }
        }
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;
        ItemSO item = inventoryItem.item;
        string description = item.ItemDescription;
        uiInventory.UpdateDescription(itemIndex, item.ItemImage, item.ItemName, description);
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            uiInventory.ShowItemAction(itemIndex);
            uiInventory.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            uiInventory.AddAction("Drop", () => DropItem(itemIndex));
        }
    }

    private void DropItem(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        EquippableItem equippableItem = inventoryItem.item as EquippableItem;

        if (equippableItem != null && itemIndex == equippedItemIndex)
        {
            equippableItem.Unequip();

            equippedItemIndex = -1;
        }

        inventoryData.RemoveItem(itemIndex);
        uiInventory.ResetSelection();
        uiInventory.HideOnlyActionPanel();
    }

    //begin pre-written code
    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject);
            uiInventory.HideOnlyActionPanel();
        }
    }
    //end pre-written code

    public void AddEquippableItemToInventory(EquippableItem equippableItem)
{
    inventoryData.AddItem(equippableItem);
    UpdateInventoryUI(inventoryData.GetCurrentInventoryState());
}

    protected void HandleItemFirstClickRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            uiInventory.ResetSelection();
            return;
        }
        uiInventory.UpdateSelection(itemIndex);
    }

    protected void ToggleInventory()
    {
        UpdateUI();
        if (!uiInventory.isActiveAndEnabled)
        {
            uiInventory.Show();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                uiInventory.UpdateData(item.Key, item.Value.item.ItemImage);
            }
        }
        else
        {
            uiInventory.Hide();
        }
    }

    private void HandleItemEquipped(EquippableItem item)
    {
        equippedItemIndex = GetEquippedItemIndex(item);
    }

    private void HandleItemUnequipped(EquippableItem item)
    {
        equippedItemIndex = -1;     
    }

    private int GetEquippedItemIndex(EquippableItem item)
    {
        for (int i = 0; i < inventoryData.Size; i++)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(i);
            int slot = inventoryItem.slotIndex;
            if (!inventoryItem.IsEmpty && inventoryItem.item == item)
            {
                return i;
            }
        }
        return -1;
    }

    private void OnDestroy()
    {
        if (inventoryData != null)
        {
            inventoryData.OnInventoryUpdated -= UpdateInventoryUI;
        }
        EquipmentEvents.OnItemEquipped -= HandleItemEquipped;
        EquipmentEvents.OnItemUnequipped -= HandleItemUnequipped;
    }
}
