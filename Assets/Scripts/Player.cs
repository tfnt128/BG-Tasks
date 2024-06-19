using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerRenderer playerRenderer;
    public IMovementInput MovementInput;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRenderer = GetComponent<PlayerRenderer>();
        MovementInput = GetComponent<IMovementInput>();
    }

    private void FixedUpdate()
    {
        playerMovement.MovePlayer(MovementInput.MovementInputVector);
        playerRenderer.RenderePlayer(MovementInput.MovementInputVector);
    }
}
