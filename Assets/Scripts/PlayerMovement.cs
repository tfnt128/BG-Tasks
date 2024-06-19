using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(Vector2 movementVector)
    {
        _rb.velocity = movementVector * movementSpeed;
    }
}
