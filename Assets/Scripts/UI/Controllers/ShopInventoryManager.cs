using UnityEngine;

public class ShopInventoryManager : InventoryManager
{
    [SerializeField] private ItemSO blueShirt;
    [SerializeField] private ItemSO yellowShirt;

    protected new void Start()
    {
        base.Start();
        InitializeShopInventory();
    }

    public void Interact()
    {
        OpenShop();
    }

    protected void OpenShop()
    {
        ToggleInventory();
    }

    private void InitializeShopInventory()
    {
        for (int i = 0; i < inventoryData.Size; i++)
        {
            inventoryData.RemoveItem(i);
        }
        for (int i = 0; i < 5; i++)
        {
            inventoryData.AddItem(blueShirt);
            inventoryData.AddItem(yellowShirt);
        }
        UpdateInventoryUI(inventoryData.GetCurrentInventoryState());
    }

    public int GetIndexOfItem(ItemSO item)
    {
        for (int i = 0; i < inventoryData.Size; i++)
        {
            if (inventoryData.GetItemAt(i).item == item)
            {
                return i;
            }
        }
        return -1;
    }

    public void RemoveItemFromInventory(int itemIndex)
    {
        inventoryData.RemoveItem(itemIndex);
        UpdateInventoryUI(inventoryData.GetCurrentInventoryState());
    }
}
