using UnityEngine;
using UnityEngine.InputSystem;
public class ControllerPlayer : MonoBehaviour
{
    public InputActionAsset inputAction;

    private InputAction moveAction;
    private InputAction LookAction;
    private InputAction jumpAction;
    private InputAction grabAction;

    [SerializeField] private Vector2 move;
    [SerializeField] private Vector2 look;

    Rigidbody rb;

    [SerializeField] float speedMove;
    [SerializeField] float lookSpeed;
    [SerializeField] float jumpForce;

    private void OnEnable()
    {
        inputAction.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        inputAction.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        LookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        grabAction = InputSystem.actions.FindAction("Grab");
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        move = moveAction.ReadValue<Vector2>();
        look = LookAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {
            Jump();
        }

        if (grabAction.WasPressedThisFrame())
        {
            Debug.Log("agarraste algo perro");
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Jump()
    {
        rb.AddForceAtPosition(new Vector3(0, jumpForce, 0), Vector3.up, ForceMode.Impulse);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + transform.forward * move.y * speedMove * Time.deltaTime);
        rb.MovePosition(rb.position + transform.right * move.x * speedMove * Time.deltaTime);
    }
}