using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public ObjectPool objectPool;

    [SerializeField] private float MaxHealth;
    [SerializeField] private float speed;
    private float currHealth;
    Rigidbody2D rb2D;
    private Transform player;
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currHealth = MaxHealth;
    }
    void OnEnable()
    {
        GameObject jugador = GameObject.FindWithTag("Player");

        if (jugador != null)
        {
            player = jugador.transform;
        }
        else
        {
            player = null;
        }
    }
    void FixedUpdate()
    {
        // Solo se mueve si encontró al jugador con éxito
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb2D.linearVelocity = direction * speed;
        }
        else
        {
            // Si el jugador no existe (por ejemplo, si el jugador también murió), se detiene
            rb2D.linearVelocity = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            currHealth--;
            if (currHealth<=0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        currHealth = MaxHealth;
        objectPool.ReturnToPool(gameObject);
    }
}
