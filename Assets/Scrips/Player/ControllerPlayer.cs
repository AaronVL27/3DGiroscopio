using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class ControllerPlayer : MonoBehaviour
{
    public InputActionAsset inputAction;


    private InputAction moveAction;
    private InputAction LookAction;
    private InputAction jumpAction;
    private InputAction grabAction;

    [SerializeField] private Vector2 move;
    [SerializeField] private Vector2 look;

    Rigidbody2D rb;

    [SerializeField] private GameObject[] weaponsPlayer;
    private int index = 0;
    [SerializeField] float speedMove;
    [SerializeField] float lookSpeed;
    [SerializeField] float jumpForce;
    Weapon weapon;
    private void OnEnable()
    {
        inputAction.FindActionMap("Player").Enable();
        GameManager.AddWeapon += ActiveWeapon;
    }
    private void OnDisable()
    {
        inputAction.FindActionMap("Player").Disable();
        GameManager.AddWeapon -= ActiveWeapon;
    }

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        LookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        grabAction = InputSystem.actions.FindAction("Grab");
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();
    }
    private void Update()
    {

        move = moveAction.ReadValue<Vector2>();
        look = LookAction.ReadValue<Vector2>();

        //if (jumpAction.WasPressedThisFrame())
        //{
        //    weapon.Shoot();
        //}
    }
    private void FixedUpdate()
    {
        Move();
    }

    //private void Jump()
    //{
    //    rb.AddForceAtPosition(new Vector3(0, jumpForce, 0), Vector3.up, ForceMode.Impulse);
    //}

    private void Move()
    {
        rb.linearVelocity = new Vector2(move.x * speedMove, move.y * speedMove);

        Vector3 direction = new Vector3(move.x, move.y, 0);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void ActiveWeapon()
    {
        if (index != weaponsPlayer.Length)
        {
            weaponsPlayer[index].SetActive(true);
            index++;
        }
        else
        {
            speedMove += 1.5f;
        }

    }

}