using System;
using UnityEngine;

public static class ItemEventManager
{
    public static event Action<EquippableItem> OnItemEquipped;

    public static void ItemEquipped(EquippableItem item)
    {
        OnItemEquipped?.Invoke(item);
    }
}
