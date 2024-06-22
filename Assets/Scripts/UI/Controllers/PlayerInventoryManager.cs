using UnityEngine;

public class PlayerInventoryManager : InventoryManager
{
    public static PlayerInventoryManager Instance { get; private set; }
    public int PlayerMoney = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OpenInventory()
    {
        ToggleInventory();
    }

    protected new void Start()
    {
        base.Start();
        InitializeShopInventory();
    }

    private void InitializeShopInventory()
    {
        for (int i = 0; i < inventoryData.Size; i++)
        {
            inventoryData.RemoveItem(i);
        }

        UpdateInventoryUI(inventoryData.GetCurrentInventoryState());
    }

    public bool CanAfford(int amount)
    {
        return PlayerMoney >= amount;
    }

    public void SpendMoney(int amount)
    {
        if (CanAfford(amount))
        {
            PlayerMoney -= amount;
        }
    }

    public void AddMoney(int amount)
    {
        PlayerMoney += amount;
    }


}
