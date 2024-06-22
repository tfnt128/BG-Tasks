using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IMovementInput
{
    public Vector2 MovementInputVector { get; private set; }
    private PlayerInventoryManager inventoryController;
    public event Action OnInteractEvent;

    private void Start() 
    {
        inventoryController = GetComponent<PlayerInventoryManager>();
    }

    private void Update()
    {
        GetInteractInput();
        GetMovementInput();
        GetInventoryToggleInput();
    }

    private void GetMovementInput()
    {
        MovementInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovementInputVector.Normalize();
    }

    private void GetInteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteractEvent?.Invoke();
        }
    }
    private void GetInventoryToggleInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryController.OpenInventory();
        }
    }
}
