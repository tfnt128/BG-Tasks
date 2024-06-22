using UnityEngine;

public class BuyableItem : ItemSO, IItemAction
{
    public string ActionName => "Buy";
    public EquippableItem equippableItem;

    public bool PerformAction(GameObject character)
    {
        PlayerInventoryManager playerInventoryController = PlayerInventoryManager.Instance;
        ShopInventoryManager shopInventoryController = character.GetComponentInParent<ShopInventoryManager>();

        if (playerInventoryController != null && shopInventoryController != null)
        {
            if (playerInventoryController.CanAfford(this.itemValue))
            {
                playerInventoryController.SpendMoney(this.itemValue);
                playerInventoryController.AddEquippableItemToInventory(equippableItem);

                int itemIndexInShop = shopInventoryController.GetIndexOfItem(this);
                if (itemIndexInShop != -1)
                {
                    shopInventoryController.RemoveItemFromInventory(itemIndexInShop);
                }

                return true;
            }
            else
            {
                Debug.Log("Not enough money to buy this item.");
            }
        }
        return false;
    }
}
