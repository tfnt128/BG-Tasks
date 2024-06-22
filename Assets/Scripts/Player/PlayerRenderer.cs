using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    public SpriteRenderer playerRenderer;
    public SpriteRenderer equipRenderer;

    public bool IsSpriteFlipped { get => playerRenderer.flipX; }

    internal void RenderePlayer(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.x) > 0.1f)
        {
            bool flipX = Vector3.Dot(transform.right, movementVector) < 0;

            playerRenderer.flipX = flipX;
            equipRenderer.flipX = flipX;
        }
    }

    public void UpdateEquipSprite(Sprite equipSprite)
    {
        equipRenderer.sprite = equipSprite;
    }
}
