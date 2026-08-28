using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    PlayerInput input;
    Rigidbody2D rb2D;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Vector2 inputMove = input.actions["Move"].ReadValue<Vector2>();

        rb2D.linearVelocity = new Vector2(inputMove.x * speed,inputMove.y * speed);

    }

}
