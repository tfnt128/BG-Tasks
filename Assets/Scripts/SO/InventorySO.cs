using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;

    [field: SerializeField]
    public int Size { get; private set; } = 10;

    //begin pre-written code
    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }
    

    public void AddItem(ItemSO item)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].IsEmpty)
            {
                AddItemToFirstFreeSlot(item, i);
                break;
            }
        }
    }

    private void AddItemToFirstFreeSlot(ItemSO item, int slotIndex)
    {
        InventoryItem newItem = new InventoryItem
        {
            item = item,
            slotIndex = slotIndex
        };

        inventoryItems[slotIndex] = newItem;
        InformAboutChange();
    }

    //end pre-written code 

    public void RemoveItem(int slotIndex)
    {
        if (inventoryItems[slotIndex].IsEmpty)
            return;

        inventoryItems[slotIndex] = InventoryItem.GetEmptyItem();
        InformAboutChange();
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (!inventoryItems[i].IsEmpty)
                returnValue[i] = inventoryItems[i];
        }
        return returnValue;
    }

    public InventoryItem GetItemAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventoryItems.Count)
            return InventoryItem.GetEmptyItem();

        return inventoryItems[slotIndex];
    }
    public int GetItemIIndexAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventoryItems.Count)
            return -1;

        return slotIndex;
    }
}

 //begin pre-written code
[Serializable]
public struct InventoryItem
{
    public ItemSO item;
    public int slotIndex;
    public bool IsEmpty => item == null;

    public static InventoryItem GetEmptyItem()
        => new InventoryItem
        {
            item = null,
            slotIndex = -1
        };
         
}
//end pre-written code
