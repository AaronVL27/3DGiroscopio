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

        if (AttitudeSensor.current != null)
            InputSystem.EnableDevice(AttitudeSensor.current);
    }

    private void FixedUpdate()
    {
        float steerInput = 0f;

        if (AttitudeSensor.current != null && AttitudeSensor.current.enabled)
        {
            float z = AttitudeSensor.current.attitude.ReadValue().eulerAngles.z;

            // z viene en 0-360. Hay que convertirlo a un rango -180 a 180
            // para que "recto" sea 0, izquierda sea negativo, derecha positivo
            if (z > 180f) z -= 360f;

            steerInput = Mathf.Clamp(z / 45f, -1f, 1f); // 45° = inclinación máxima que consideras "a fondo"
        }

        rb2D.linearVelocity = new Vector2(steerInput * speed, 0f); // el auto avanza solo, y se mueve lateral según inclinación
    }

}
