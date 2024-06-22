using UnityEngine;
using UnityEngine.Events;

public static class EquipmentEvents
{
    public static UnityAction<EquippableItem> OnItemEquipped;
    public static UnityAction<EquippableItem> OnItemUnequipped;

    public static void EquipItem(EquippableItem item)
    {
        OnItemEquipped?.Invoke(item);
    }

    public static void UnequipItem(EquippableItem item)
    {
        OnItemUnequipped?.Invoke(item);
    }
}
