using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerRenderer playerRenderer;
    public IMovementInput MovementInput;
    public PlayerAnimations playerAnimations;
    public PlayerAIInteractions playerAiInteractions;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRenderer = GetComponent<PlayerRenderer>();
        MovementInput = GetComponent<IMovementInput>();
        playerAnimations = GetComponent<PlayerAnimations>();
        playerAiInteractions = GetComponent<PlayerAIInteractions>();
        MovementInput.OnInteractEvent += () => playerAiInteractions.Interact(playerRenderer.IsSpriteFlipped);
    }

    private void FixedUpdate()
    {
        playerMovement.MovePlayer(MovementInput.MovementInputVector);
        playerRenderer.RenderePlayer(MovementInput.MovementInputVector);
        playerAnimations.SetupAnimations(MovementInput.MovementInputVector);
    }
}