using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float tiltRange = 0.2f; // sensibilidad de la inclinación

    Rigidbody2D rb2D;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    private void FixedUpdate()
    {
        float steerInput = 0f;

        if (Accelerometer.current != null && Accelerometer.current.enabled)
        {
            float x = Accelerometer.current.acceleration.ReadValue().x;
            steerInput = Mathf.Clamp(x / tiltRange, -1f, 1f);
        }

        rb2D.linearVelocity = new Vector2(steerInput * speed, 0f);
    }

}
