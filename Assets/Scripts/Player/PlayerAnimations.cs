using System.Collections;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private SpriteRenderer equipSpriteRenderer;
    [SerializeField] private SpriteDataSO walkSpritesData;
    [SerializeField] private SpriteDataSO equipSpriteData;
    [SerializeField] private float walkFrameDuration = 0.2f;

    private bool isWalking = false;
    private int currentWalkFrame = 0;
    private Coroutine walkCoroutine;

    private void Start()
    {
        UpdateEquip();
        EquipmentEvents.OnItemUnequipped += OnItemUnequipped;
    }

    private void OnDestroy()
    {
        EquipmentEvents.OnItemUnequipped -= OnItemUnequipped;
    }

    private void UpdateEquip()
    {
        playerSpriteRenderer.sprite = walkSpritesData.standardRenderer;
        equipSpriteRenderer.sprite = equipSpriteData.standardRenderer;
    }

    public void SetupAnimations(Vector2 movementVector)
    {
        isWalking = movementVector.magnitude > 0;

        if (isWalking)
        {
            if (walkCoroutine == null)
            {
                StartWalkAnimation();
            }
        }
        else
        {
            if (walkCoroutine != null)
            {
                StopWalkAnimation();
                ResetToStandardSprites();
            }
        }
    }

    private void StartWalkAnimation()
    {
        walkCoroutine = StartCoroutine(AnimateWalk());
    }

    private void StopWalkAnimation()
    {
        StopCoroutine(walkCoroutine);
        walkCoroutine = null;
    }

    private IEnumerator AnimateWalk()
    {
        while (isWalking)
        {
            playerSpriteRenderer.sprite = walkSpritesData.sprites[currentWalkFrame];
            equipSpriteRenderer.sprite = equipSpriteData.sprites[currentWalkFrame];
            currentWalkFrame = (currentWalkFrame + 1) % walkSpritesData.sprites.Length;
            yield return new WaitForSeconds(walkFrameDuration);
        }
    }

    private void ResetToStandardSprites()
    {
        playerSpriteRenderer.sprite = walkSpritesData.standardRenderer;
        equipSpriteRenderer.sprite = equipSpriteData.standardRenderer;
    }

    public void UpdateEquipAnimation(SpriteDataSO newEquipSpriteData)
    {
        equipSpriteData = newEquipSpriteData;
        if (!isWalking)
        {
            equipSpriteRenderer.sprite = equipSpriteData.standardRenderer;
        }
    }

    private void OnItemUnequipped(EquippableItem item)
    {
        NoClothes();
    }

    public void NoClothes()
    {
        equipSpriteData = walkSpritesData;
        equipSpriteRenderer.sprite = walkSpritesData.standardRenderer;
    }
}
