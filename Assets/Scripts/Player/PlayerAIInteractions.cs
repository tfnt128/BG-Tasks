using UnityEngine;

public class PlayerAIInteractions : MonoBehaviour
{
    [SerializeField] private Transform raycastPoint;

    public void Interact(bool isSpriteFlipped)
    {
        Debug.DrawRay(raycastPoint.position, isSpriteFlipped ? Vector3.left : Vector3.right, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(raycastPoint.position, isSpriteFlipped ? Vector3.left : Vector3.right, 1);
        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<ShopInventoryManager>())
            {
                hit.collider.GetComponent<ShopInventoryManager>().Interact();
            }
        }

    }
}
