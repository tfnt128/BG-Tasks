using UnityEngine;

public class PlayerEquipHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerRenderer;
    [SerializeField] private PlayerAnimations playerAnimations;

    public void UpdateEquipAnimation(SpriteDataSO equipSpriteData)
    {
        if (playerAnimations != null)
        {
            playerAnimations.UpdateEquipAnimation(equipSpriteData);
        }
    }
}
