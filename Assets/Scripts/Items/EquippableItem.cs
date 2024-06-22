using UnityEngine;
//begin pre-written code
[CreateAssetMenu(menuName = "Items/Equippable Item")]
public class EquippableItem : ItemSO, IDestroyableItem, IItemAction
{
    public string ActionName => "Equip";
    public SpriteDataSO equipSpriteData;

    public bool PerformAction(GameObject character)
    {
        PlayerEquipHandler equipHandler = character.GetComponent<PlayerEquipHandler>();
        if (equipHandler != null)
        {
            EquipmentEvents.EquipItem(this);
            equipHandler.UpdateEquipAnimation(equipSpriteData);
            return true;
        }
        return false;
    }

    public void Unequip()
    {
        EquipmentEvents.UnequipItem(this);
    }
}
//end pre-written code
